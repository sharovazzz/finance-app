using PersonalFinanceApp.Interfaces;
using PersonalFinanceApp.Models;

namespace PersonalFinanceApp.Repository
{
    public class UserMemoryRepository : IUserRepository
    {
        private readonly List<User> _users = new List<User>();
        private int userId = 1;
        private int categoryId = 5;

        public List<User> GetAllUsers()
        {
            return _users;
        }

        public User GetUser(int id)
        {
            return _users.FirstOrDefault(u => u.Id == id);
        }

        public List<User> GetUsersByEmailOrPhone(string email, string phone)
        {
            return _users.Where(u => 
                (!string.IsNullOrEmpty(email) && u.Email == email) || 
                (!string.IsNullOrEmpty(phone) && u.Phone == phone))
                .ToList();
        }

        public User CreateUser(UserDto userDto)
        {
            var user = new User
            {
                Id = userId++,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email.Trim().ToLowerInvariant(),
                Phone = userDto.Phone,
                Categories = DefaultCategory.GetDefaultCategories()
            };

            _users.Add(user);
            return user;
        }

        public void UpdateUser(int id, UserDto userDto)
        {
            var userToUpdate = _users.FirstOrDefault(u => u.Id == id);

            userToUpdate.FirstName = userDto.FirstName;
            userToUpdate.LastName = userDto.LastName;
            userToUpdate.Email = userDto.Email.Trim().ToLowerInvariant();
            userToUpdate.Phone = userDto.Phone;
        }

        public void DeleteUser(int id)
        {
            var userToDelete = _users.FirstOrDefault(u => u.Id == id);

            _users.Remove(userToDelete);
        }

        public void ResetUserCategories(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);

            user.Categories = DefaultCategory.GetDefaultCategories();
        }

        public void DeleteUserCategory(int userId, int categoryId)
        {
            var user = _users.FirstOrDefault(u => u.Id == userId);
            
            var category = user.Categories.FirstOrDefault(c => c.Id == categoryId);

            user.Categories.Remove(category);
        }

        public void CreateUserCategory(int userId, CreateCategoryDto createCategoryDto)
        {
            var user = _users.FirstOrDefault(u => u.Id == userId);

            var category = new Category
            {
                Id = categoryId++,
                Name = createCategoryDto.Name
            };

            user.Categories.Add(category);
        }
    }
}
