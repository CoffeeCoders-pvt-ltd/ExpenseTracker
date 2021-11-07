using System;
using System.Linq;
using System.Threading.Tasks;
using ExpenseTracker.Common.Model;
using ExpenseTracker.Core.Dto.Workspace;
using ExpenseTracker.Core.Entities;
using ExpenseTracker.Core.Repositories.Interface;
using ExpenseTracker.Core.Services.Interface;
using ExpenseTracker.Infrastructure.Extensions;
using ExpenseTracker.Web.Provider;
using ExpenseTracker.Web.ViewModels.Workspace;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Web.Controllers
{
    public class WorkspaceController : Controller
    {
        private readonly IWorkspaceService _workspaceService;
        private readonly IWorkspaceRepository _workspaceRepository;
        private readonly IUserProvider _userProvider;

        public WorkspaceController(IWorkspaceService workspaceService, IWorkspaceRepository workspaceRepository,
            IUserProvider userProvider)
        {
            _workspaceService = workspaceService;
            _workspaceRepository = workspaceRepository;
            _userProvider = userProvider;
        }

        // GET
        public async Task<IActionResult> Index(string name)
        {
            var workspaceViewModel = new WorkspaceIndexViewModel
            {
                Workspaces = await _workspaceRepository.GetAllAsync(x => x.Status == BaseModel.StatusActive)
                    .ConfigureAwait(true)
            };
            return View(workspaceViewModel);
        }

        public IActionResult Create()
        {
            var workspaceViewModel = new WorkspaceViewModel();
            return View(workspaceViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(WorkspaceViewModel workspaceViewModel)
        {
            try
            {
                if (!ModelState.IsValid) return View(workspaceViewModel);

                var currentUser = await GetCurrentUser().ConfigureAwait(true);
                var workspaceDto = new WorkspaceCreateDto()
                {
                    UserId = currentUser.Id,
                    Color = workspaceViewModel.Color,
                    Name = workspaceViewModel.WorkspaceName,
                    Description = workspaceViewModel.Description
                };

                await _workspaceService.Create(workspaceDto).ConfigureAwait(true);

                HttpContext?.Session.SetDefaultWorkspace(currentUser.DefaultWorkspace.Token);

                var defaultWorkspace = HttpContext?.Session.GetDefaultWorkspace();

                this.AddSuccessMessage("Workspace Created Successfully.");
            }
            catch (Exception e)
            {
                this.AddErrorMessage(e.Message);
            }

            return RedirectToAction(nameof(Index), "Home");
        }

        public async Task<IActionResult> ChangeDefault(string workspaceToken, string redirectUrl = "/")
        {
            try
            {
                await _workspaceService.ChangeDefault(workspaceToken).ConfigureAwait(true);
                this.AddSuccessMessage("Workspace Changed Successfully");
            }
            catch (Exception e)
            {
                this.AddErrorMessage(e.Message);
            }

            return LocalRedirectPreserveMethod(redirectUrl);
        }

        private async Task<User> GetCurrentUser()
        {
            return await _userProvider.GetCurrentUser();
        }

        public async Task<IActionResult> Deactivate(long id)
        {
            try
            {
                var workshop = await _workspaceRepository.FindOrThrowAsync(id);
                var currentUser = await _userProvider.GetCurrentUser();
                await _workspaceService.Deactivate(workshop);
                var activeWorkspaces =
                    await _workspaceRepository.GetAllAsync(w =>
                        w.User == currentUser && w.Status == BaseModel.StatusActive);
                if (activeWorkspaces.Count is 0)
                {
                    this.AddSuccessMessage("successfully moved to the trash");
                    return RedirectToAction(nameof(Create));
                }

                var availableToken = activeWorkspaces.FirstOrDefault();
                await ChangeDefault(availableToken.Token);
                this.AddSuccessMessage("successfully moved to the trash");
                return RedirectToAction(nameof(Index)); 
            }
            catch (Exception e)
            {
                this.AddErrorMessage(e.Message);
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> Activate(long id)
        {
            try
            {
                var workshop = await _workspaceRepository.FindOrThrowAsync(id);
                await _workspaceService.Activate(workshop);
                this.AddSuccessMessage("Activated");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                this.AddErrorMessage(e.Message);
                return RedirectToAction("Index");
            }
        }

        private int GetCurrentUserId() => _userProvider.GetCurrentUserId();
    }
}