using System.Threading.Tasks;
using ExpenseTracker.Core.Dto.TransactionCategory;
using ExpenseTracker.Core.Entities;
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

        public async Task Create(TransactionCategoryCreateDto dto)
        {
            using var tx = TransactionScopeHelper.GetInstance();
            var transactionCategory = new TransactionCategory(dto.Workspace, dto.Type, dto.Name, dto.Color, dto.Icon);
            await _transactionCategoryRepository.CreateAsync(transactionCategory);
            await _uow.CommitAsync();
            tx.Complete();
        }

        public async Task Update(TransactionCategory transactionCategory, TransactionCategoryUpdateDto dto)
        {
            using var tx = TransactionScopeHelper.GetInstance();
            transactionCategory.Update(dto.Name, dto.Color, dto.Icon, dto.Type);
            _transactionCategoryRepository.Update(transactionCategory);
            await _uow.CommitAsync();
            tx.Complete();
        }

        public async Task Delete(TransactionCategory transactionCategory)
        {
            using var tx = TransactionScopeHelper.GetInstance();
            _transactionCategoryRepository.Delete(transactionCategory);
            await _uow.CommitAsync();
            tx.Complete();
        }
    }
}