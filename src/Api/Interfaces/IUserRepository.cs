using PersonalFinanceApp.Models;

namespace PersonalFinanceApp.Interfaces
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();
        User GetUser(int id);
        List<User> GetUsersByEmailOrPhone(string email, string phone);
        User CreateUser(UserDto userDto);
        void UpdateUser(int id, UserDto userDto);
        void DeleteUser(int id);
        void ResetUserCategories(int id);
        void DeleteUserCategory(int userId, int categoryId);
        void CreateUserCategory(int userId, CreateCategoryDto createCategoryDto);
    }
}
