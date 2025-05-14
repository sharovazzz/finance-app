namespace PersonalFinanceApp.Models
{
    public class CreateBudgetDto
    {
        public decimal Amount { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
    }
}
