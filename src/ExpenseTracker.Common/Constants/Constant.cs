namespace ExpenseTracker.Common.Constants
{
    public class Constant
    {
        public const string StatusActive = "Active";
        public const string StatusInactive = "Inactive";
        public const string StatusDeleted = "Deleted";

        public static readonly string[] StatusList = new[] { StatusActive, StatusInactive, StatusDeleted };
    }
}