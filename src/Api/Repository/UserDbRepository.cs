using Microsoft.EntityFrameworkCore;
using PersonalFinanceApp.Data;
using PersonalFinanceApp.Interfaces;
using PersonalFinanceApp.Models;

namespace PersonalFinanceApp.Repository
{
    public class UserDbRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserDbRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<User> GetAllUsers()
        {
            return _context.Users.Include(u => u.Categories).ToList();
        }

        public User GetUser(int id)
        {
            return _context.Users.Include(u => u.Categories).FirstOrDefault(u => u.Id == id);
        }

        public List<User> GetUsersByEmailOrPhone(string email, string phone)
        {
            return _context.Users
                .Where(u => (!string.IsNullOrEmpty(email) && u.Email == email) ||
                            (!string.IsNullOrEmpty(phone) && u.Phone == phone))
                .ToList();
        }

        public User CreateUser(UserDto userDto)
        {
            var user = new User
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email.Trim().ToLowerInvariant(),
                Phone = userDto.Phone,
                Categories = DefaultCategory.GetDefaultCategories().Select(c => new Category { Name = c.Name }).ToList()
            };

            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public void UpdateUser(int id, UserDto userDto)
        {
            var userToUpdate = _context.Users.FirstOrDefault(u => u.Id == id);

            userToUpdate.FirstName = userDto.FirstName ?? userToUpdate.FirstName;
            userToUpdate.LastName = userDto.LastName ?? userToUpdate.LastName;
            userToUpdate.Email = userDto.Email ?? userToUpdate.Email;
            userToUpdate.Phone = userDto.Phone ?? userToUpdate.Phone;

            _context.SaveChanges();
        }

        public void DeleteUser(int id)
        {
            var userToDelete = _context.Users.FirstOrDefault(u => u.Id == id);

            _context.Users.Remove(userToDelete);
            _context.SaveChanges();
            
        }

        public void ResetUserCategories(int id)
        {
            var user = _context.Users.Include(u => u.Categories).FirstOrDefault(u => u.Id == id);
            
            user.Categories = DefaultCategory.GetDefaultCategories().Select(c => new Category { Name = c.Name }).ToList();
            _context.SaveChanges();
        }

        public void DeleteUserCategory(int userId, int categoryId)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            var category = _context.Categories.FirstOrDefault(c => c.Id == categoryId);
            
            user.Categories.Remove(category);
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

