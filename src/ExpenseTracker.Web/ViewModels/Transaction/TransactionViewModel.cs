using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ExpenseTracker.Core.Entities.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExpenseTracker.Web.ViewModels.Transaction
{
    public class TransactionViewModel
    {
        [Required]
        [DisplayName("Amount")]
        public decimal TransactionAmount { get; set; }
        
        [DisplayName("Transaction Date")]
        public DateTime TransactionEntryDate { get; set; }

        [Display(Name = "Transaction Category")]
        public int TransactionCategoryId { get; set; }

        [DisplayName("Transaction Type")]
        public string Type { get; set; }
        public long Id { get; set; }

        
        [DisplayName("Transaction Proof")] 
        public IFormFile? File { get; set; }

        public SelectList TransactionTypes => new SelectList(TransactionType.ValidTypes);

        public IList<Core.Entities.TransactionCategory> TransactionCategories { get; set; } =
            new List<Core.Entities.TransactionCategory>();

        public SelectList TransactionCategoriesSelectList =>
            new SelectList(TransactionCategories, "Id", "CategoryName", TransactionCategoryId);

        [DisplayName("Description")]
        public string? Description { get; set; }
    }
}