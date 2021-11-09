using ExpenseTracker.Core.Dto.Transaction;
using System.Threading.Tasks;
using ExpenseTracker.Core.Entities;

namespace ExpenseTracker.Core.Services.Interface
{
    public interface ITransactionService
    {
        Task Create(TransactionCreateDto dto);
        Task Update(TransactionUpdateDto transactionUpdateDto);
        Task Remove(Transaction transaction);
    }
}