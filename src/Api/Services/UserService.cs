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

            
            var createUserEmail = createUserDto.Email.Trim().ToLowerInvariant();
            var createUserPhone = PhoneValidation(createUserDto.Phone);
            var anyUser = _userRepository.GetUsersByEmailOrPhone(createUserEmail, createUserPhone);

            if (anyUser != null)
            {
                throw new ArgumentException("User with this email or phone already exists.");
            }

            var userDto = new UserDto
            {
                FirstName = createUserDto.FirstName ?? string.Empty,
                LastName = createUserDto.LastName ?? string.Empty,
                Email = createUserEmail ?? string.Empty,
                Phone = createUserPhone ?? string.Empty
            };

            return _userRepository.CreateUser(userDto);
            
        }

        public void UpdateUser(int id, UserDto userDto)
        {
            var user = _userRepository.GetUser(id);
            var updateUserEmail = userDto.Email.Trim().ToLowerInvariant();
            var updateUserPhone = PhoneValidation(userDto.Phone);
            var anyUser = _userRepository.GetUsersByEmailOrPhone(updateUserEmail, updateUserPhone);
            
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            if (string.IsNullOrEmpty(userDto.Email) && string.IsNullOrEmpty(userDto.Phone))
            {
                throw new ArgumentException("Either email or phone must be provided.");
            }

            if (anyUser != null && anyUser.Id != id)
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

        public void ResettingUserCategories(int id)
        {
            var user = _userRepository.GetUser(id);

            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            _userRepository.ResettingUserCategories(id);
        }

        public void DeleteUserCategory(int userId, int categoryId)
        {
            var user = _userRepository.GetUser(userId);
            var category = user.Categories.FirstOrDefault(c => c.Id == categoryId);

            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            if (category == null)
            {
                throw new KeyNotFoundException("Category not found");
            }

            _userRepository.DeleteUserCategory(userId, categoryId);
        }

        public void CreateUserCategory(int userId, CategoryDto categoryDto)
        {
            var user = _userRepository.GetUser(userId);

            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            _userRepository.CreateUserCategory(userId, categoryDto);
        }

        public string PhoneValidation(string phone)
        {
            var digits = new string(phone.Where(p => char.IsDigit(p)).ToArray());
            var lenght = digits.Length;
            
            if (lenght < 10 || lenght > 11 || (lenght == 11 && !digits.StartsWith("7") && !digits.StartsWith("8")))
            {
                throw new ArgumentException("Incorrect phone number");
            }

            return _userRepository.PhoneValidation(phone);
        }
    }   
}
