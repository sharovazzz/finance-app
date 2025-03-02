using PersonalFinanceApp.Interfaces;
using PersonalFinanceApp.Models;
using PersonalFinanceApp.Repository;
using PersonalFinanceApp.Services;

namespace PersonalFinanceApp.Tests
{
    public class UserServiceTests
    {
        private readonly IUserRepository _userRepository;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _userRepository = new UserMemoryRepository();
            _userService = new UserService(_userRepository);
        }

        [Fact]
        public void CreateUser_EmptyEmailAndPhone_Exeption()
        {
            var createUserDto = new CreateUserDto
            {
                FirstName = "FirstName",
                LastName = "LastName",
                Email = "",
                Phone = ""
            };

            Action act = () => _userService.CreateUser(createUserDto);

            ArgumentException exception = Assert.Throws<ArgumentException>(act);
            Assert.Equal("Either email or phone must be provided.", exception.Message);

        }

        [Fact]
        public void CreateUser_IncorrectPhone_Exeption()
        {
            var createUserDto = new CreateUserDto
            {
                FirstName = "FirstName",
                LastName = "LastName",
                Email = "",
                Phone = "123"
            };

            Action act = () => _userService.CreateUser(createUserDto);

            ArgumentException exception = Assert.Throws<ArgumentException>(act);
            Assert.Equal("Incorrect phone number.", exception.Message);
        }

        [Fact]
        public void CerateUser_UserWithThisPhoneAlredyExists_Exeption()
        {
            var createUserDto = new CreateUserDto
            {
                FirstName = "FirstName",
                LastName = "LastName",
                Email = "",
                Phone = "79961110000"
            };
            _userService.CreateUser(createUserDto);

            Action act = () => _userService.CreateUser(createUserDto);

            ArgumentException exeption = Assert.Throws<ArgumentException>(act);
            Assert.Equal("User with this email or phone already exists.", exeption.Message);
        }

        [Fact]
        public void CreateUser_UserWithThisEmailAlredyExists_Exeption()
        {
            var createUserDto = new CreateUserDto
            {
                FirstName = "FirstName",
                LastName = "LastName",
                Email = "na@gmail.com",
                Phone = ""
            };
            _userService.CreateUser(createUserDto);

            Action act = () => _userService.CreateUser(createUserDto);

            ArgumentException exeption = Assert.Throws<ArgumentException>(act);
            Assert.Equal("User with this email or phone already exists.", exeption.Message);
        }

        [Fact]
        public void CreateUser_ValidData_NotNull()
        {
            var createUserDto = new CreateUserDto
            {
                FirstName = "FirstName",
                LastName = "LastName",
                Email = "na@gmail.com",
                Phone = "9961110000"
            };

            var result = _userService.CreateUser(createUserDto);

            Assert.NotNull(result);
        }

        [Fact]
        public void CreateUser_NotFormattedPhone_FormattedPhone()
        {
            var createUserDto = new CreateUserDto
            {
                FirstName = "FirstName",
                LastName = "LastName",
                Email = "",
                Phone = "9961110000"
            };

            var result = _userService.CreateUser(createUserDto);

            Assert.Equal("79961110000", result.Phone);
        }

        [Fact]
        public void CreateUser_NotFormattedEmail_FormattedEmail()
        {
            var createUserDto = new CreateUserDto
            {
                FirstName = "FirstName",
                LastName = "LastName",
                Email = "NaN@gmail.com",
                Phone = ""
            };

            var result = _userService.CreateUser(createUserDto);

            Assert.Equal("nan@gmail.com", result.Email);
        }
    }
}