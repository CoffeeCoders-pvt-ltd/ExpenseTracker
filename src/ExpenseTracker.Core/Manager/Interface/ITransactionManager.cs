using System.Threading.Tasks;
using ExpenseTracker.Core.Dto.Transaction;
using ExpenseTracker.Core.Entities;
using Microsoft.AspNetCore.Http;

namespace ExpenseTracker.Core.Manager.Interface
{
    public interface ITransactionManager
    {
        Task RecordTransaction(IFormFile? file, TransactionCreateDto dto);
        Task RemoveTransaction(Transaction transaction);
    }
}