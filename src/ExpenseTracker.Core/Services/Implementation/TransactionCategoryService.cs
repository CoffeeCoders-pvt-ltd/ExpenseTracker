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

        public TransactionCategoryService(ITransactionCategoryRepository transactionCategoryRepository, IUow uow)
        {
            _transactionCategoryRepository = transactionCategoryRepository;
            _uow = uow;
        }
        public async Task Create(TransactionCategoryCreateDto transactionCategoryCreateDto)
        {
            using var tx = TransactionScopeHelper.GetInstance();

            var transaction = TransactionCategory.Create(transactionCategoryCreateDto.Type, transactionCategoryCreateDto.Name, transactionCategoryCreateDto.Color,
                transactionCategoryCreateDto.Icon);
            await _transactionCategoryRepository.CreateAsync(transaction).ConfigureAwait(false);
            await _uow.CommitAsync();
            tx.Complete();
        }

        public async Task Update(TransactionCategoryUpdateDto transactionCategoryUpdateDto)
        {
            using var tx = TransactionScopeHelper.GetInstance();

            var transaction = await _transactionCategoryRepository.GetByIdAsync(transactionCategoryUpdateDto.TransactionCategoryId).ConfigureAwait(false) ?? throw new TransactionCategoryNotFoundException();
            transaction.UpdateName(transactionCategoryUpdateDto.Name);
            transaction.UpdateColor(transactionCategoryUpdateDto.Color);
            transaction.UpdateIcon(transactionCategoryUpdateDto.Icon);

            _transactionCategoryRepository.Update(transaction);
            await _uow.CommitAsync();
            tx.Complete();
        }

        public async Task Delete(long transactionCategoryId)
        {
            using var tx = TransactionScopeHelper.GetInstance();

            var transaction = await _transactionCategoryRepository.GetByIdAsync(transactionCategoryId).ConfigureAwait(false) ?? throw new TransactionCategoryNotFoundException();

            _transactionCategoryRepository.Delete(transaction);
            await _uow.CommitAsync();
            tx.Complete();
        }
    }
}