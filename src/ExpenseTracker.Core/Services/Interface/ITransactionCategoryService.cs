using System.Threading.Tasks;
using ExpenseTracker.Core.Dto.TransactionCategory;
using ExpenseTracker.Core.Entities;

namespace ExpenseTracker.Core.Services.Interface
{
    public interface ITransactionCategoryService
    {
        Task Create(TransactionCategoryCreateDto dto);
        Task Update(TransactionCategory transactionCategory, TransactionCategoryUpdateDto dto);
        Task Delete(TransactionCategory transactionCategory);
    }
}