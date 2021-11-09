using ExpenseTracker.Core.Dto.Transaction;
using ExpenseTracker.Core.Repositories.Interface;
using ExpenseTracker.Core.Services.Interface;
using System.Threading.Tasks;
using ExpenseTracker.Common.Helpers;
using ExpenseTracker.Core.Entities;
using ExpenseTracker.Core.Exceptions;
using ExpenseTracker.Common.DBAL;

namespace ExpenseTracker.Core.Services.Implementation
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IUow _uow;

        public TransactionService(ITransactionRepository transactionRepository, IUow uow)
        {
            _transactionRepository = transactionRepository;
            _uow = uow;
        }

        public async Task Create(TransactionCreateDto dto)
        {
            using var tx = TransactionScopeHelper.GetInstance();
            var transaction = Transaction.Create(dto.Workspace, dto.TransactionCategory, dto.Amount,
                dto.TransactionDate, dto.Type, dto.TransactionProof, dto.Description);
            transaction.Description = dto.Description;
            await _transactionRepository.CreateAsync(transaction);
            await _uow.CommitAsync();
            tx.Complete();
        }

        public async Task Remove(Transaction transaction)
        {
            using var tx = TransactionScopeHelper.GetInstance();
            _transactionRepository.Flush(transaction);
            await _uow.CommitAsync();
            tx.Complete();
        }

        public async Task Update(TransactionUpdateDto transactionUpdateDto)
        {
            var transactionExists =
                await _transactionRepository.CheckIfExistAsync(a => a.Id == transactionUpdateDto.Id);
            if (!transactionExists) throw new TransactionNotFoundException(transactionUpdateDto.Id);

            using var tx = TransactionScopeHelper.GetInstance();

            var transaction = await _transactionRepository.FindAsync(transactionUpdateDto.Id) ??
                              throw new TransactionNotFoundException();
            transaction.UpdateAmount(transactionUpdateDto.Amount);
            transaction.UpdateTransactionDate(transactionUpdateDto.TransactionDate);

            _transactionRepository.Update(transaction);
            await _uow.CommitAsync();
            tx.Complete();
        }
    }
}