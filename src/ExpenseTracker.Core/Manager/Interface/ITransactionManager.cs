using System.Threading.Tasks;
using ExpenseTracker.Core.Dto.Transaction;
using Microsoft.AspNetCore.Http;

namespace ExpenseTracker.Core.Manager.Interface
{
    public interface ITransactionManager
    {
        Task RecordTransaction(IFormFile? file, TransactionCreateDto dto);
    }
}