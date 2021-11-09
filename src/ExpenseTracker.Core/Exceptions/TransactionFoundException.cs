using System;

namespace ExpenseTracker.Core.Exceptions
{
    public class TransactionFoundException : Exception
    {
        public TransactionFoundException(string message = "Transaction Category has transaction.") : base(message)
        {
        }
    }
}