using System.Threading.Tasks;
using ExpenseTracker.Core.Dto.TransactionCategory;
using ExpenseTracker.Core.Entities;
using ExpenseTracker.Core.Exceptions;
using ExpenseTracker.Common.Helpers;
using ExpenseTracker.Core.Repositories.Interface;
using ExpenseTracker.Core.Services.Interface;
using ExpenseTracker.Common.DBAL;

namespace ExpenseTracker.Core.Services.Implementation
{
    public class TransactionCategoryService : ITransactionCategoryService
    {
        private readonly ITransactionCategoryRepository _transactionCategoryRepository;
        private readonly IUow _uow;
        private readonly ITransactionRepository _transactionRepository;

        public TransactionCategoryService(ITransactionCategoryRepository transactionCategoryRepository, IUow uow,
            ITransactionRepository transactionRepository)
        {
            _transactionCategoryRepository = transactionCategoryRepository;
            _uow = uow;
            _transactionRepository = transactionRepository;
        }

        public async Task Create(TransactionCategoryCreateDto transactionCategoryCreateDto)
        {
            using var tx = TransactionScopeHelper.GetInstance();

            var transaction = TransactionCategory.Create(transactionCategoryCreateDto.Type,
                transactionCategoryCreateDto.Name, transactionCategoryCreateDto.Color,
                transactionCategoryCreateDto.Icon);
            await _transactionCategoryRepository.CreateAsync(transaction);
            await _uow.CommitAsync();
            tx.Complete();
        }

        public async Task Update(TransactionCategoryUpdateDto transactionCategoryUpdateDto)
        {
            using var tx = TransactionScopeHelper.GetInstance();

            var transaction =
                await _transactionCategoryRepository.FindAsync(transactionCategoryUpdateDto.TransactionCategoryId) ??
                throw new TransactionCategoryNotFoundException();
            transaction.UpdateName(transactionCategoryUpdateDto.Name);
            transaction.UpdateColor(transactionCategoryUpdateDto.Color);
            transaction.UpdateIcon(transactionCategoryUpdateDto.Icon);

            _transactionCategoryRepository.Update(transaction);
            await _uow.CommitAsync();
            tx.Complete();
        }

        public async Task Delete(TransactionCategory transactionCategory)
        {
            using var tx = TransactionScopeHelper.GetInstance();
            var existTransaction = await _transactionRepository.ExistTransaction(transactionCategory.Id);
            if (existTransaction) throw new TransactionFoundException();
            _transactionCategoryRepository.Delete(transactionCategory);
            await _uow.CommitAsync();
            tx.Complete();
        }
    }
}