using Microsoft.EntityFrameworkCore;
using PersonalFinanceApp.Data;
using PersonalFinanceApp.Interfaces;
using PersonalFinanceApp.Models;

namespace PersonalFinanceApp.Repository
{
    public class UserCategoryDbRepository : IUserCategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public UserCategoryDbRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void ResetUserCategories(int id)
        {
            var user = _context.Users.Include(u => u.Categories).FirstOrDefault(u => u.Id == id);

            user.Categories = DefaultCategory.GetDefaultCategories().Select(c => new Category { Name = c.Name }).ToList();
            _context.SaveChanges();
        }

        public void DeleteUserCategory(int userId, int categoryId)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == categoryId);

            _context.Categories.Remove(category);
            _context.SaveChanges();
        }

        public Category CreateUserCategory(int userId, CreateCategoryDto createCategoryDto)
        {
            var user = _context.Users.Include(u => u.Categories).FirstOrDefault(u => u.Id == userId);

            var category = new Category
            {
                Name = createCategoryDto.Name
            };

            user.Categories.Add(category);
            _context.SaveChanges();

            return category;
        }

    }
}
