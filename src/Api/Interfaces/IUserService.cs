using PersonalFinanceApp.Models;

namespace PersonalFinanceApp.Interfaces
{
    public interface IUserService
    {
        List<User> GetAllUsers();
        User GetUser(int id);
        User CreateUser(CreateUserDto createUserDto);
        void UpdateUser(int id, UserDto userDto);
        void DeleteUser(int id);
        void ResettingUserCategories(int id);
        void DeleteUserCategory(int userId, int categoryId);
        void CreateUserCategory(int userId, CategoryDto categoryDto);
        string PhoneValidation(string phone);
    }
}
