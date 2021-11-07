using System.Collections.Generic;
using System.Threading.Tasks;
using ExpenseTracker.Common.Constants;
using ExpenseTracker.Core.Entities;
using ExpenseTracker.Core.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Infrastructure.Repositories.Implementation
{
    public class WorkspaceRepository : GenericRepository<Workspace>, IWorkspaceRepository
    {
        public WorkspaceRepository(DbContext context) : base(context)
        {
        }

        public async Task<Workspace> GetDefaultWorkspace(long userId)
            => await GetPredicatedQueryable(a =>
                    a.WorkspaceType == Workspace.TypeDefaultWorkspace && a.UserId == userId)
                .SingleOrDefaultAsync();

        public async Task<Workspace> GetByToken(string token)
        {
            return await GetPredicatedQueryable(a => a.Token == token)
                .SingleOrDefaultAsync();
        }

        public async Task<List<Workspace>> GetActiveWorkspaces(long userId)
            => await GetAllAsync(x => x.UserId == userId && x.Status == StatusConstants.StatusActive);

        public async Task<bool> HasDefaultWorkspace(long userId)
            => await CheckIfExistAsync(w =>
                w.WorkspaceType == Workspace.TypeDefaultWorkspace && w.Status == StatusConstants.StatusActive &&
                w.UserId == userId);
    }
}