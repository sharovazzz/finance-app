using PersonalFinanceApp.Models;

namespace PersonalFinanceApp.Interfaces
{
    public interface IUserRepository
    {
        List<ShortUser> GetAllUsers();
        User GetUser(int id);
        List<User> GetUsersByEmailOrPhone(string email, string phone);
        User CreateUser(UserDto userDto);
        void UpdateUser(int id, UserDto userDto);
        void DeleteUser(int id);
    }
}
