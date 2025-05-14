namespace PersonalFinanceApp.Models
{
    public class Category
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public decimal BudgetAmount { get; set; }
        public decimal Spent { get; set; }
        public decimal RemainsBudget => BudgetAmount - Spent;
    }
}
