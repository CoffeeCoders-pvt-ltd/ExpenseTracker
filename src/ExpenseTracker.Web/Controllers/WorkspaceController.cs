using System;
using System.Linq;
using System.Threading.Tasks;
using ExpenseTracker.Common.Constants;
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
            var currentUser = await _userProvider.GetCurrentUser();
            var workspaceViewModel = new WorkspaceIndexViewModel
            {
                Workspaces = await _workspaceRepository.GetActiveWorkspaces(currentUser.Id)
            };
            return View(workspaceViewModel);
        }

        public IActionResult Create()
        {
            var workspaceViewModel = new WorkspaceViewModel();
            return View(workspaceViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(WorkspaceViewModel vm)
        {
            try
            {
                if (!ModelState.IsValid) return View(vm);

                var currentUser = await GetCurrentUser();
                var workspaceDto = new WorkspaceCreateDto()
                {
                    UserId = currentUser.Id,
                    Color = vm.Color,
                    Name = vm.WorkspaceName,
                    Description = vm.Description,
                    Icon = vm.Icon
                };

                await _workspaceService.Create(workspaceDto);

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

        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            try
            {
                var workspace = await _workspaceRepository.FindOrThrowAsync(id);
                var workspaceEditVm = new WorkspaceEditViewModel()
                {
                    Color = workspace.Color,
                    Name = workspace.WorkSpaceName,
                    Description = workspace.Description,
                    Icon = workspace.Icon
                };
                return View(workspaceEditVm);
            }
            catch (Exception e)
            {
                this.AddErrorMessage(e.Message);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(long id, WorkspaceEditViewModel vm)
        {
            try
            {
                if (!ModelState.IsValid) return View(vm);
                var workspace = await _workspaceRepository.FindOrThrowAsync(id);
                var updateDto = new WorkspaceUpdateDto()
                {
                    Color = vm.Color,
                    Name = vm.Name,
                    Description = vm.Description,
                    Icon = vm.Icon
                };
                await _workspaceService.Update(workspace,updateDto);
                this.AddSuccessMessage("Workspace updated");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                this.AddErrorMessage(e.Message);
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> ChangeDefault(string workspaceToken, string redirectUrl = "/")
        {
            try
            {
                await _workspaceService.ChangeDefault(workspaceToken);
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
                await _workspaceService.Deactivate(workshop);
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