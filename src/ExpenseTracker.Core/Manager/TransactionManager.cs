﻿using System;
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
        private string identity = "";

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
                identity = await _fileManager.SaveImage(file, Guid.NewGuid().ToString(), nameof(Transaction));
            }

            dto.TransactionProof = identity;
            await _transactionService.Create(dto);
            tsc.Complete();
        }

        public async Task RemoveTransaction(Entities.Transaction transaction)
        {
            using var tsc = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            if (!string.IsNullOrEmpty(transaction.TransactionProof))
            {
                _fileManager.RemoveImage(transaction.TransactionProof, nameof(Transaction));
            }

            await _transactionService.Remove(transaction);

            tsc.Complete();
        }
    }
}