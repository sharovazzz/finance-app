using Microsoft.AspNetCore.Http.HttpResults;
using PersonalFinanceApp.Interfaces;
using PersonalFinanceApp.Models;

namespace PersonalFinanceApp.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> _users = new List<User>();
        private int userId = 1;
        public List<User> GetAllUsers()
        {
            return _users;
        }

        public User GetUser(int id)
        {
            return _users.FirstOrDefault(u => u.Id == id);
        }
        public void CreateUser(UserDto userDto)
        {
            var user = new User
            {
                Id = userId++,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                Phone = userDto.Phone
            };

            _users.Add(user);
        }
        public void UpdateUser(int id, UserDto userDto) 
        {
            var userToUpdate = _users.FirstOrDefault(u => u.Id == id);

            if (userToUpdate != null)
            {
                userToUpdate.FirstName = userDto.FirstName;
                userToUpdate.LastName = userDto.LastName;  
                userToUpdate.Email = userDto.Email;
                userToUpdate.Phone = userDto.Phone;
            }

        }
        public void DeleteUser(int id) 
        {
            var userToDelete = _users.FirstOrDefault(u => u.Id == id);
            if (userToDelete != null) 
            {
                _users.Remove(userToDelete);
            }
        }
    }
}
