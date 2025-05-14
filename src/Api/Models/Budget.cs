namespace PersonalFinanceApp.Models
{
    public class Budget
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public decimal TotalSpend { get; set; }
        public decimal RemainsBudget { get; set; }
    }
}
