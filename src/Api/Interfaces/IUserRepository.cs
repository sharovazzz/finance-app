using PersonalFinanceApp.Models;

namespace PersonalFinanceApp.Interfaces
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();
        User GetUser(int id);
        void CreateUser(UserDto userDto);
        void UpdateUser(int id, UserDto userDto);
        void DeleteUser(int id);
    }
}
