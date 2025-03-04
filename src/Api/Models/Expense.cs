namespace PersonalFinanceApp.Models
{
    public class Expense
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public int CategoryId { get; set; }
        public DateTime Date { get; set; }
        public string Address {  get; set; }
        public string Description { get; set; }

    }
}
