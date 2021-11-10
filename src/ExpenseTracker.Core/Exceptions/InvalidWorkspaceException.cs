using System;

namespace ExpenseTracker.Core.Exceptions
{
    public class InvalidWorkspaceException : Exception
    {
        public InvalidWorkspaceException(string message = "Invalid Workspace") : base(message)
        {
        }
    }
}