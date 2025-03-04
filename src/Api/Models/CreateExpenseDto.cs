namespace PersonalFinanceApp.Models
{
    public class CreateExpenseDto
    {
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
    }
}
