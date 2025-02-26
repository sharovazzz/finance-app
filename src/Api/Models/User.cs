namespace PersonalFinanceApp.Models
{
    public class User
    {
        public required int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public required List<Category> Categories { get; set; } = new List<Category>();
    }
}
