using PersonalFinanceApp.Interfaces;
using PersonalFinanceApp.Models;
using PersonalFinanceApp.Repository;
using PersonalFinanceApp.Services;

namespace PersonalFinanceApp.Tests.Service
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
        public void CreateUser_EmptyEmailAndPhone_Exсeption()
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
        public void CreateUser_IncorrectPhone_Exсeption()
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
        public void CreateUser_UserWithThisPhoneAlreadyExists_Exception()
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

            ArgumentException exception = Assert.Throws<ArgumentException>(act);
            Assert.Equal("User with this email or phone already exists.", exception.Message);
        }

        [Fact]
        public void CreateUser_UserWithThisEmailAlreadyExists_Exception()
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

            ArgumentException exception = Assert.Throws<ArgumentException>(act);
            Assert.Equal("User with this email or phone already exists.", exception.Message);
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

        [Fact]
        public void UpdateUser_NotFoundUser_Exception()
        {
            var updateUserDto = new UserDto
            {
                FirstName = "Name",
                LastName = "Name",
                Email = "na@gmail.com",
                Phone = ""
            };

            Action act = () => _userService.UpdateUser(1, updateUserDto);

            KeyNotFoundException exception = Assert.Throws<KeyNotFoundException>(act);
            Assert.Equal("User not found", exception.Message);
        }

        [Fact]
        public void UpdateUser_EmptyEmailAndPhone_Exception()
        {
            var createUserDto = new UserDto
            {
                FirstName = "FirstName",
                LastName = "LastName",
                Email = "na@gmail.com",
                Phone = "79961110000"
            };
            var newUser = _userRepository.CreateUser(createUserDto);

            var updateUserDto = new UserDto
            {
                FirstName = "Name",
                LastName = "Name",
                Email = "",
                Phone = ""
            };
            
            Action act = () => _userService.UpdateUser(newUser.Id, updateUserDto);

            ArgumentException exception = Assert.Throws<ArgumentException>(act);
            Assert.Equal("Either email or phone must be provided.", exception.Message);
        }

        [Fact]
        public void UpdateUser_IncorrectPhone_Exception()
        {
            var createUserDto = new UserDto
            {
                FirstName = "FirstName",
                LastName = "LastName",
                Email = "",
                Phone = "79961110000"
            };
            var newUser = _userRepository.CreateUser(createUserDto);

            var updateUserDto = new UserDto
            {
                FirstName = "Name",
                LastName = "Name",
                Email = "",
                Phone = "123"
            };

            Action act = () => _userService.UpdateUser(newUser.Id, updateUserDto);

            ArgumentException exception = Assert.Throws<ArgumentException>(act);
            Assert.Equal("Incorrect phone number.", exception.Message);
        }

        [Fact]
        public void UpdateUser_UserWithThisPhoneAlredyExists_Exception()
        {
            var createAnyUserDto = new UserDto
            {
                FirstName = "FirstName",
                LastName = "LastName",
                Email = "",
                Phone = "79961110000"
            };
            _userRepository.CreateUser(createAnyUserDto);

            var createUserDto = new UserDto
            {
                FirstName = "FirstName",
                LastName = "LastName",
                Email = "na@gmail.com",
                Phone = ""
            };
            var secondUser = _userRepository.CreateUser(createUserDto);

            var updateUserDto = new UserDto
            {
                FirstName = "Name",
                LastName = "Name",
                Email = "na@gmail.com",
                Phone = "79961110000"
            };

            Action act = () => _userService.UpdateUser(secondUser.Id, updateUserDto);

            ArgumentException exception = Assert.Throws<ArgumentException>(act);
            Assert.Equal("User with this email or phone already exists.", exception.Message);
        }

        [Fact]
        public void UpdateUser_ValidData_NotNull()
        {
            var createUserDto = new UserDto
            {
                FirstName = "FirstName",
                LastName = "LastName",
                Email = "na@gmail.com",
                Phone = ""
            };
            var newUser = _userRepository.CreateUser(createUserDto);

            var updateUserDto = new UserDto
            {
                FirstName = "Name",
                LastName = "Name",
                Email = "na@gmail.com",
                Phone = "89961110000"
            };

            _userService.UpdateUser(newUser.Id, updateUserDto);
            var updatedUser = _userRepository.GetUser(1);

            Assert.NotNull(updatedUser);
        }

        [Fact]
        public void UpdateUser_NotFormattedPhone_FormattedPhone()
        {
            var createUserDto = new UserDto
            {
                FirstName = "FirstName",
                LastName = "LastName",
                Email = "na@gmail.com",
                Phone = ""
            };
            var newUser = _userRepository.CreateUser(createUserDto);

            var updateUserDto = new UserDto
            {
                FirstName = "Name",
                LastName = "Name",
                Email = "na@gmail.com",
                Phone = "89961110000"
            };

            _userService.UpdateUser(newUser.Id, updateUserDto);
            var updatedUser = _userRepository.GetUser(1);

            Assert.Equal("79961110000", updatedUser.Phone);
        }

        [Fact]
        public void UpdateUser_NotFormattedEmail_FormattedEmail()
        {
            var createUserDto = new UserDto
            {
                FirstName = "FirstName",
                LastName = "LastName",
                Email = "",
                Phone = "79961110000"
            };
            var newUser = _userRepository.CreateUser(createUserDto);

            var updateUserDto = new UserDto
            {
                FirstName = "Name",
                LastName = "Name",
                Email = "Na@gmail.com",
                Phone = "79961110000"
            };

            _userService.UpdateUser(newUser.Id, updateUserDto);
            var updatedUser = _userRepository.GetUser(1);

            Assert.Equal("na@gmail.com", updatedUser.Email);
        }
    }
}