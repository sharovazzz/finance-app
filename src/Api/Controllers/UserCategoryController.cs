using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalFinanceApp.Interfaces;
using PersonalFinanceApp.Models;

namespace PersonalFinanceApp.Controllers
{
    [Route("api/users/{userId}/category")]
    [ApiController]
    public class UserCategoryController : Controller
    {
        private readonly IUserService _userService;
        public UserCategoryController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPut("reset")]
        public IActionResult ResettingUserCategories(int userId)
        {
            try
            {
                _userService.ResettingUserCategories(userId);
                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{categoryId}")]
        public IActionResult DeleteUserCategory(int userId, int categoryId)
        {
            try
            {
                _userService.DeleteUserCategory(userId, categoryId);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult CreateUserCategory(int userId, CategoryDto categoryDto)
        {
            try
            {
                _userService.CreateUserCategory(userId, categoryDto);
                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
