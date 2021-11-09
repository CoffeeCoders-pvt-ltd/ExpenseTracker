using System.Threading.Tasks;
using ExpenseTracker.Core.Dto.TransactionCategory;
using ExpenseTracker.Core.Entities;

namespace ExpenseTracker.Core.Services.Interface
{
    public interface ITransactionCategoryService
    {
        Task Create(TransactionCategoryCreateDto transactionCategoryCreateDto);
        Task Update(TransactionCategoryUpdateDto transactionCategoryUpdateDto);
        Task Delete(TransactionCategory transactionCategory);
    }
}