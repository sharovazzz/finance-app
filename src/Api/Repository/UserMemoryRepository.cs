using PersonalFinanceApp.Interfaces;
using PersonalFinanceApp.Models;

namespace PersonalFinanceApp.Repository
{
    public class UserMemoryRepository : IUserRepository
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

        public User CreateUser(UserDto userDto)
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
            return user;
        }

        public void UpdateUser(int id, UserDto userDto)
        {
            var userToUpdate = _users.FirstOrDefault(u => u.Id == id);

            if (userToUpdate == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            userToUpdate.FirstName = userDto.FirstName;
            userToUpdate.LastName = userDto.LastName;
            userToUpdate.Email = userDto.Email;
            userToUpdate.Phone = userDto.Phone;

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
