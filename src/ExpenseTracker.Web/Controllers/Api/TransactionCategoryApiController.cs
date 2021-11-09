using System;
using System.Threading.Tasks;
using ExpenseTracker.Core.Entities.Common;
using ExpenseTracker.Core.Repositories.Interface;
using ExpenseTracker.Web.Provider;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Web.Controllers.Api
{
    [ApiController]
    [Route("/api/TransactionCategory/")]
    public class TransactionCategoryApiController : ControllerBase
    {
        private readonly ITransactionCategoryRepository _transactionCategoryRepository;
        private readonly IUserProvider _userProvider;

        public TransactionCategoryApiController(ITransactionCategoryRepository transactionCategoryRepository,
            IUserProvider userProvider)
        {
            _transactionCategoryRepository = transactionCategoryRepository;
            _userProvider = userProvider;
        }

        [HttpGet("{type}/getByType")]
        public async Task<IActionResult> GetCategoriesByType(string type)
        {
            try
            {
                var defaultWorkspaceId = (await _userProvider.GetCurrentUser()).DefaultWorkspace.Id;
                if (!TransactionType.IsValidType(type))
                {
                    return UnprocessableEntity("Transaction type not valid");
                }

                var transactionCategories = await _transactionCategoryRepository.GetByType(type, defaultWorkspaceId);

                return Ok(transactionCategories);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}