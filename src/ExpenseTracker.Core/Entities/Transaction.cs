using System;
using System.ComponentModel.DataAnnotations.Schema;
using ExpenseTracker.Common.Model;
using ExpenseTracker.Core.Entities.Common;
using ExpenseTracker.Core.Exceptions;

namespace ExpenseTracker.Core.Entities
{
    [Table("transaction", Schema = "core")]
    public class Transaction : BaseModel
    {
        public decimal Amount { get; protected set; }

        public DateTime TransactionDate { get; protected set; }

        public DateTime EntryDate { get; protected set; }
        public string? Description { get; set; }

        public string Type { get; protected set; }
        
        public long TransactionCategoryId { get; protected set; }
        public virtual TransactionCategory TransactionCategory { get; protected set; }
        public virtual Workspace Workspace { get; protected set; }
        public long WorkspaceId { get; protected set; }
        

        protected Transaction()
        {
        }

        public Transaction(Workspace workspace, TransactionCategory transactionCategory, decimal amount,
            DateTime transactionDate, string type)
        {
            SetWorkspace(workspace);
            SetTransactionCategory(transactionCategory);
            if (amount <= 0) throw new InvalidTransactionAmountException();
            Amount = amount;
            EntryDate = DateTime.Now;
            TransactionDate = transactionDate;
            if (!TransactionType.IsValidType(type)) throw new InvalidTransactionTypeException(type);
            Type = type;
        }

        public static Transaction Create(Workspace workspace, TransactionCategory transactionCategory, decimal amount,
            DateTime transactionDate, string type)
        {
            return new(workspace, transactionCategory, amount, transactionDate, type);
        }

        public virtual void UpdateAmount(decimal amount)
        {
            if (amount <= 0) throw new InvalidTransactionAmountException();
            Amount = amount;
        }

        public void UpdateTransactionDate(DateTime transactionDate)
        {
            TransactionDate = transactionDate;
        }


        private void SetTransactionCategory(TransactionCategory transactionCategory)
        {
            TransactionCategory = transactionCategory;
        }


        private void SetWorkspace(Workspace workspace)
        {
            Workspace = workspace;
        }
    }
}