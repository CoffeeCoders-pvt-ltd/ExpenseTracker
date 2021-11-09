using System.Threading.Tasks;
using ExpenseTracker.Core.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Infrastructure.Repositories.Implementation
{
    public class TransactionRepository : GenericRepository<Core.Entities.Transaction>, ITransactionRepository
    {
        public TransactionRepository(DbContext context) : base(context)
        {
        }

        public async Task<bool> ExistTransaction(long transactionCategoryId)
            => await CheckIfExistAsync(t => t.TransactionCategoryId == transactionCategoryId);
    }
}