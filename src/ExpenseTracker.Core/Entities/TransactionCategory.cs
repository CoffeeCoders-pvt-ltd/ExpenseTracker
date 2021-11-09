using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ExpenseTracker.Common.Model;
using ExpenseTracker.Core.Entities.Common;
using ExpenseTracker.Core.Exceptions;

namespace ExpenseTracker.Core.Entities
{
    [Table("transaction_category", Schema = "core")]
    public class TransactionCategory : BaseModel
    {
        public virtual Workspace Workspace { get; protected set; }
        public long WorkspaceId { get; protected set; }
        public string Type { get; protected set; }
        public virtual List<Transaction> Transactions { get; set; } = new List<Transaction>();
        public string CategoryName { get; protected set; }
        public string Color { get; protected set; }
        public string Icon { get; protected set; }

        protected TransactionCategory()
        {
        }

        public TransactionCategory(Workspace workspace, string type, string categoryName, string color, string icon)
        {
            if (!TransactionType.IsValidType(type)) throw new InvalidTransactionTypeException(type);
            Workspace = workspace;
            Copy(categoryName, color, icon, type);
        }

        public void Update(string categoryName, string color, string icon, string type)
            => Copy(categoryName, color, icon, type);


        private void Copy(string categoryName, string color, string icon, string type)
        {
            CategoryName = categoryName;
            Color = color;
            Icon = icon;
            Type = type;
        }


        public virtual void UpdateName(string name) => CategoryName = name;


        public virtual void UpdateColor(string color) => Color = color;

        public virtual void UpdateIcon(string icon) => Icon = icon;
    }
}