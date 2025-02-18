using Microsoft.AspNetCore.Mvc;
using PersonalFinanceApp.Interfaces;
using PersonalFinanceApp.Models;

namespace PersonalFinanceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _userRepository.GetAllUsers();

            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var user = _userRepository.GetUser(id);

            if (user == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            { 
                return BadRequest(ModelState);
            }

            return Ok(user);
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] UserDto userDto)
        {
            if (userDto == null)
            {
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_userRepository.GetAllUsers().Any(u => u.Email == userDto.Email || u.Phone == userDto.Phone))
            {
                return BadRequest("User with this email or phone already exists.");
            }

            _userRepository.CreateUser(userDto);
            return Ok(userDto);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _userRepository.GetUser(id);

            if (user == null)
            {
                return NotFound();
            }

            _userRepository.DeleteUser(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UserDto userDto)
        {

            var user = _userRepository.GetUser(id);

            if (user == null)
            {  
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_userRepository.GetAllUsers().Any(u => u.Email == userDto.Email || u.Phone == userDto.Phone))
            {
                return BadRequest("User with this email or phone already exists.");
            }

            _userRepository.UpdateUser(id, userDto);
            return Ok(userDto);
        }
    }
}
