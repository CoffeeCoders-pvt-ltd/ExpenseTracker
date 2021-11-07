using System.Threading.Tasks;
using ExpenseTracker.Core.Dto.Workspace;
using ExpenseTracker.Core.Entities;

namespace ExpenseTracker.Core.Services.Interface
{
    public interface IWorkspaceService
    {
        Task Create(WorkspaceCreateDto workspaceCreateDto);
        Task Update(WorkspaceUpdateDto workspaceUpdateDto);
        Task Delete(int workspaceId);
        Task ChangeDefault(string workspaceToken);

        Task Deactivate(Workspace workspace);
        Task Activate(Workspace workspace);

    }
}