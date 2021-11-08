using System.Threading.Tasks;
using System.Transactions;
using ExpenseTracker.Core.Dto.Transaction;
using ExpenseTracker.Core.FileManager.Interface;
using ExpenseTracker.Core.Manager.Interface;
using ExpenseTracker.Core.Services.Interface;
using Microsoft.AspNetCore.Http;

namespace ExpenseTracker.Core.Manager
{
    public class TransactionManager : ITransactionManager
    {
        private readonly ITransactionService _transactionService;
        private readonly IFileManager _fileManager;

        public TransactionManager(ITransactionService transactionService, IFileManager fileManager)
        {
            _transactionService = transactionService;
            _fileManager = fileManager;
        }

        public async Task RecordTransaction(IFormFile? file, TransactionCreateDto dto)
        {
            using var tsc = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            if (file != null)
            {
                var transaction = await _transactionService.Create(dto);
                await _fileManager.SaveImage(file, transaction.Id.ToString(), nameof(transaction));
            }
            else if (file == null)
            {
                await _transactionService.Create(dto);
            }

            tsc.Complete();
        }
    }
}