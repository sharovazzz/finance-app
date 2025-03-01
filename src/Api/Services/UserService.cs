using PersonalFinanceApp.Interfaces;
using PersonalFinanceApp.Models;
using PersonalFinanceApp.Helpers;

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

            var createUserEmail = createUserDto.Email?.Trim().ToLowerInvariant();
            var createUserPhone = createUserDto.Phone;

            if (!string.IsNullOrEmpty(createUserPhone) && !PhoneHelpers.TryParseAsPhone(createUserPhone, out createUserPhone))
            {
                throw new ArgumentException("Incorrect phone number.");
            }

            var usersWithThisPhoneOrEmail = _userRepository.GetUsersByEmailOrPhone(createUserEmail, createUserPhone);

            if (usersWithThisPhoneOrEmail.Any())
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
            
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }
            
            if (string.IsNullOrEmpty(userDto.Email) && string.IsNullOrEmpty(userDto.Phone))
            {
                throw new ArgumentException("Either email or phone must be provided.");
            }

            var updateUserEmail = userDto.Email?.Trim().ToLowerInvariant();
            var updateUserPhone = userDto.Phone;

            if (!string.IsNullOrEmpty(updateUserPhone) && !PhoneHelpers.TryParseAsPhone(updateUserPhone, out updateUserPhone))
            {
                throw new ArgumentException("Incorrect phone number.");
            }

            var usersWithThisPhoneOrEmail = _userRepository.GetUsersByEmailOrPhone(updateUserEmail, updateUserPhone);
            
            if (usersWithThisPhoneOrEmail.Any(u => u.Id != id))
            {
                throw new ArgumentException("User with this email or phone already exists.");
            }

            userDto.Phone = updateUserPhone;
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

        public void ResetUserCategories(int id)
        {
            var user = _userRepository.GetUser(id);

            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            _userRepository.ResetUserCategories(id);
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

        public Category CreateUserCategory(int userId, CreateCategoryDto createCategoryDto)
        {
            var user = _userRepository.GetUser(userId);

            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            return _userRepository.CreateUserCategory(userId, createCategoryDto);
        }
    }   
}
