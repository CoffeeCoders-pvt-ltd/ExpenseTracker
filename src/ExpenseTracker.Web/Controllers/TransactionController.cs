using System;
using System.Linq;
using System.Threading.Tasks;
using ExpenseTracker.Core.Dto.Transaction;
using ExpenseTracker.Core.Dto.TransactionCategory;
using ExpenseTracker.Core.Entities;
using ExpenseTracker.Core.Exceptions;
using ExpenseTracker.Core.Manager.Interface;
using ExpenseTracker.Core.Repositories.Interface;
using ExpenseTracker.Core.Services.Interface;
using ExpenseTracker.Infrastructure.Extensions;
using ExpenseTracker.Web.Provider;
using ExpenseTracker.Web.ViewModels;
using ExpenseTracker.Web.ViewModels.Transaction;
using ExpenseTracker.Web.ViewModels.TransactionCategory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ExpenseTracker.Web.Controllers
{
    [Authorize]
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;
        private readonly ITransactionCategoryRepository _transactionCategoryRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly ILogger<TransactionController> _logger;
        private readonly IUserProvider _userProvider;
        private readonly ITransactionManager _transactionManager;
        private readonly IWorkspaceRepository _workspaceRepository;

        public TransactionController(ITransactionService transactionService,
            ITransactionCategoryRepository transactionCategoryRepository,
            ITransactionRepository transactionRepository,
            ILogger<TransactionController> logger, IUserProvider userProvider, ITransactionManager transactionManager,
            IWorkspaceRepository workspaceRepository)
        {
            _transactionService = transactionService;
            _transactionCategoryRepository = transactionCategoryRepository;
            _transactionRepository = transactionRepository;
            _logger = logger;
            _userProvider = userProvider;
            _transactionManager = transactionManager;
            _workspaceRepository = workspaceRepository;
        }

        public async Task<IActionResult> Index(TransactionIndexViewModel transactionIndexViewModel)
        {
            var defaultWorkspaceToken = (await _userProvider.GetCurrentUser()).DefaultWorkspace.Token;
            var transactions = _transactionRepository
                .GetPredicatedQueryable(a => a.Workspace.Token == defaultWorkspaceToken)
                .OrderByDescending(x => x.TransactionDate).ToList();
            transactionIndexViewModel.Transactions = transactions;
            return View(transactionIndexViewModel);
        }

        public async Task<IActionResult> Create()
        {
            var transactionViewModel = new TransactionViewModel
            {
                TransactionCategories = await _transactionCategoryRepository.GetAllAsync()
            };
            return View(transactionViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TransactionViewModel transactionViewModel)
        {
            try
            {
                if (!ModelState.IsValid) return View(transactionViewModel);
                var user = await _userProvider.GetCurrentUser();
                var workspace = await _workspaceRepository.GetByToken(user.DefaultWorkspace.Token)
                                ?? throw new WorkspaceNotFoundException();
                var transactionCategory =
                    await _transactionCategoryRepository.FindAsync(transactionViewModel.TransactionCategoryId)
                    ?? throw new TransactionCategoryNotFoundException();

                var dto = new TransactionCreateDto()
                {
                    Workspace = workspace,
                    TransactionDate = transactionViewModel.TransactionEntryDate,
                    Amount = transactionViewModel.TransactionAmount,
                    TransactionCategory = transactionCategory,
                    Type = transactionViewModel.Type,
                    Description = transactionViewModel.Description
                };
                await _transactionManager.RecordTransaction(transactionViewModel.File, dto);
                this.AddSuccessMessage("Transaction Created Successfully");
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
                var transaction = await _transactionRepository.FindAsync(id) ??
                                  throw new TransactionNotFoundException();

                var transactionViewModel = new TransactionViewModel()
                {
                    TransactionAmount = transaction.Amount,
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
        public async Task<IActionResult> Edit(TransactionViewModel transactionViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    await _transactionService.Update(new TransactionUpdateDto()
                    {
                        Amount = transactionViewModel.TransactionAmount,
                        Id = transactionViewModel.Id,
                    });
                }

                this.AddSuccessMessage("Transaction  Updated Successfully");
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
                var transaction = await _transactionRepository.FindAsync(id) ?? throw new TransactionNotFoundException();
                await _transactionManager.RemoveTransaction(transaction);
                this.AddSuccessMessage("Transaction  Deleted Successfully");
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                this.AddErrorMessage(e.Message);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}