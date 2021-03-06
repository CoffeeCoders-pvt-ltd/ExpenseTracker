namespace ExpenseTracker.Core.Dto.TransactionCategory
{
    public class TransactionCategoryCreateDto
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Icon { get; set; }
        public string Color { get; set; }
        public Entities.Workspace Workspace { get; set; }
    }
}