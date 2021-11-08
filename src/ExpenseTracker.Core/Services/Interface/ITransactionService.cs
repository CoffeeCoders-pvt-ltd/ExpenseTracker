using ExpenseTracker.Core.Dto.Transaction;
using System.Threading.Tasks;
using ExpenseTracker.Core.Entities;

namespace ExpenseTracker.Core.Services.Interface
{
   public interface ITransactionService
    {
        Task<Transaction> Create(TransactionCreateDto transactionCreateDto);
        Task Update(TransactionUpdateDto transactionUpdateDto);
        Task Delete(long transactionId);
    }
}
