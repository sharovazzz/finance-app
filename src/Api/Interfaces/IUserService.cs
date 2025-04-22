using PersonalFinanceApp.Models;

namespace PersonalFinanceApp.Interfaces
{
    public interface IUserService
    {
        List<ShortUser> GetAllUsers();
        User GetUser(int id);
        User CreateUser(CreateUserDto createUserDto);
        void UpdateUser(int id, UserDto userDto);
        void DeleteUser(int id);
    }
}
