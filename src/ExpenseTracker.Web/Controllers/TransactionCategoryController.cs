using System;
using System.Threading.Tasks;
using ExpenseTracker.Core.Dto.TransactionCategory;
using ExpenseTracker.Core.Exceptions;
using ExpenseTracker.Core.Repositories.Interface;
using ExpenseTracker.Core.Services.Interface;
using ExpenseTracker.Infrastructure.Extensions;
using ExpenseTracker.Web.Provider;
using ExpenseTracker.Web.ViewModels.TransactionCategory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ExpenseTracker.Web.Controllers
{
    [Authorize]
    public class TransactionCategoryController : Controller
    {
        private readonly ITransactionCategoryRepository _transactionCategoryRepository;
        private readonly ITransactionCategoryService _transactionCategoryService;
        private readonly ILogger<TransactionCategoryController> _logger;
        private readonly IUserProvider _userProvider;

        public TransactionCategoryController(ITransactionCategoryRepository transactionCategoryRepository,
            ITransactionCategoryService transactionCategoryService,
            ILogger<TransactionCategoryController> logger, IUserProvider userProvider)
        {
            _transactionCategoryRepository = transactionCategoryRepository;
            _transactionCategoryService = transactionCategoryService;
            _logger = logger;
            _userProvider = userProvider;
        }

        public async Task<IActionResult> Index(TransactionCategoryIndexViewModel transactionCategoryIndexViewModel)
        {
            var defaultWorkspaceId = await GetDefaultWorkspaceId();
            var transactionCategories =
                await _transactionCategoryRepository.GetCategoriesGetByWorkspace(defaultWorkspaceId);
            transactionCategoryIndexViewModel.TransactionCategories = transactionCategories;
            return View(transactionCategoryIndexViewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var transactionCategoryViewModel = new TransactionCategoryViewModel();
            return View(transactionCategoryViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TransactionCategoryViewModel transactionCategoryVm)
        {
            try
            {
                if (!ModelState.IsValid) return View(transactionCategoryVm);
                var currentUser = await _userProvider.GetCurrentUser();
                var dto = new TransactionCategoryCreateDto()
                {
                    Workspace = currentUser.DefaultWorkspace,
                    Color = transactionCategoryVm.Color,
                    Type = transactionCategoryVm.Type,
                    Name = transactionCategoryVm.Name,
                    Icon = transactionCategoryVm.Icon
                };
                await _transactionCategoryService.Create(dto);
                this.AddSuccessMessage("Transaction Category Create Successfully");
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                this.AddErrorMessage(e.Message);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(long id)
        {
            try
            {
                var transactionCategory = await _transactionCategoryRepository.FindAsync(id) ??
                                          throw new TransactionCategoryNotFoundException();
                var defaultWorkspaceId = await GetDefaultWorkspaceId();
                if (transactionCategory.WorkspaceId != defaultWorkspaceId) return RedirectToAction(nameof(Index));
                var transactionViewModel = new TransactionCategoryViewModel()
                {
                    Name = transactionCategory.CategoryName,
                    Icon = transactionCategory.Icon,
                    Type = transactionCategory.Type,
                    Color = transactionCategory.Color
                };

                return View(transactionViewModel);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                this.AddErrorMessage(e.Message);
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(long id, TransactionCategoryViewModel transactionCategoryViewModel)
        {
            try
            {
                if (!ModelState.IsValid) return View(transactionCategoryViewModel);
                var transactionCategory = await _transactionCategoryRepository.FindAsync(id) ??
                                          throw new TransactionCategoryNotFoundException();
                var defaultWorkspaceId = await GetDefaultWorkspaceId();
                if (transactionCategory.WorkspaceId != defaultWorkspaceId) return RedirectToAction(nameof(Index));
                var dto = new TransactionCategoryUpdateDto()
                {
                    Name = transactionCategoryViewModel.Name,
                    Color = transactionCategoryViewModel.Color,
                    Icon = transactionCategory.Icon,
                    Type = transactionCategory.Type
                };
                await _transactionCategoryService.Update(transactionCategory, dto);
                this.AddSuccessMessage("Transaction Category Updated Successfully");
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                this.AddErrorMessage(e.Message);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var transactionCategory = await _transactionCategoryRepository.FindAsync(id) ?? throw new TransactionCategoryNotFoundException();
                var defaultWorkspaceId = await GetDefaultWorkspaceId();
                if (transactionCategory.WorkspaceId != defaultWorkspaceId) return RedirectToAction(nameof(Index));
                await _transactionCategoryService.Delete(transactionCategory);
                this.AddSuccessMessage("Transaction Category Deleted Successfully");
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                this.AddErrorMessage(e.Message);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<long> GetDefaultWorkspaceId() => (await _userProvider.GetCurrentUser()).DefaultWorkspace.Id;
    }
}