namespace PersonalFinanceApp.Models
{
    public class DefaultCategory
    {
        public static List<Category> GetDefaultCategories() 
        {
            return new List<Category>
            {
                new Category {Id = 1, Name = "Food"},
                new Category {Id = 2, Name = "Entertainment"},
                new Category {Id = 3, Name = "Clothes"},
                new Category {Id = 4, Name = "Transport"}
            };
        }
    }
}
