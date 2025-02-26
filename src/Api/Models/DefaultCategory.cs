namespace PersonalFinanceApp.Models
{
    public class DefaultCategory
    {
        public static List<Category> GetDefaultCategories() 
        {
            return new List<Category>
            {

                new Category {Name = "Food"},
                new Category {Name = "Entertainment"},
                new Category {Name = "Clothes"},
                new Category {Name = "Transport"}

            };
        }
    }
}
