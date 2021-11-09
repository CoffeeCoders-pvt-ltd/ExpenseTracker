using System.Collections.Generic;
using System.Threading.Tasks;
using ExpenseTracker.Core.Entities;
using ExpenseTracker.Core.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Infrastructure.Repositories.Implementation
{
    public class TransactionCategoryRepository : GenericRepository<TransactionCategory>, ITransactionCategoryRepository
    {
        public TransactionCategoryRepository(DbContext context) : base(context)
        {
        }

        public async Task<IList<TransactionCategory>> GetByType(string type, long workspaceId)
        {
            return await GetPredicatedQueryable(a => a.Type == type && a.WorkspaceId == workspaceId).ToListAsync();
        }

        public async Task<IList<TransactionCategory>> GetCategoriesGetByWorkspace(long workspaceId)
            => await GetAllAsync(x => x.WorkspaceId == workspaceId);
    }
}