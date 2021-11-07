namespace ExpenseTracker.Common.Model
{
    public class BaseModel
    {
        public long Id { get; set; }
        public string Status { get; set; } = "Active";
    }
}