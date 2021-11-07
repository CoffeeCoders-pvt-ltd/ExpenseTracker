using System.Transactions;

namespace ExpenseTracker.Common.Helpers
{
    public static class TransactionScopeHelper
    {
        public static TransactionScope GetInstance() 
            => new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
    }
}