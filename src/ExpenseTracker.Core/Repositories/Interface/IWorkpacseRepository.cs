using System.Collections.Generic;
using System.Threading.Tasks;
using ExpenseTracker.Common.Repositories.Interface;
using ExpenseTracker.Core.Entities;

namespace ExpenseTracker.Core.Repositories.Interface
{
    public interface IWorkspaceRepository : IGenericRepository<Workspace>
    {
        Task<Workspace> GetDefaultWorkspace(long userId);
        Task<Workspace> GetByToken(string token);

        Task<List<Workspace>> GetActiveWorkspaces(long userId);
    }
}