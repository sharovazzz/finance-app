using PersonalFinanceApp.Interfaces;
using PersonalFinanceApp.Models;

namespace PersonalFinanceApp.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public List<User> GetAllUsers()
        {
            return _userRepository.GetAllUsers();
        }
        
        public User GetUser(int id)
        {
            return _userRepository.GetUser(id);
        }

        public User CreateUser(CreateUserDto createUserDto)
        {
            if (string.IsNullOrEmpty(createUserDto.Email) && string.IsNullOrEmpty(createUserDto.Phone))
            {
                throw new ArgumentException("Either email or phone must be provided.");
            }

            if (_userRepository.GetAllUsers().Any(u => u.Email == createUserDto.Email || u.Phone == createUserDto.Phone))
            {
                throw new ArgumentException("User with this email or phone already exists.");
            }

            var userDto = new UserDto
            {
                FirstName = createUserDto.FirstName ?? string.Empty,
                LastName = createUserDto.LastName ?? string.Empty,
                Email = createUserDto.Email ?? string.Empty,
                Phone = createUserDto.Phone ?? string.Empty
            };

            return _userRepository.CreateUser(userDto);
            
        }

        public void UpdateUser(int id, UserDto userDto)
        {
            var user = _userRepository.GetUser(id);

            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            if (string.IsNullOrEmpty(userDto.Email) && string.IsNullOrEmpty(userDto.Phone))
            {
                throw new ArgumentException("Either email or phone must be provided.");
            }

            if (_userRepository.GetAllUsers().Any(u => u.Id != id && (u.Email == userDto.Email || u.Phone == userDto.Phone)))
            {
                throw new ArgumentException("User with this email or phone already exists.");
            }

            _userRepository.UpdateUser(id, userDto);
        }

        public void DeleteUser(int id)
        {
            var user = _userRepository.GetUser(id);

            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            _userRepository.DeleteUser(id);
        }
    }   
}
