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
        public IActionResult ResetUserCategories(int userId)
        {
            try
            {
                _userService.ResetUserCategories(userId);
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
        public IActionResult CreateUserCategory(int userId, CreateCategoryDto createCategoryDto)
        {
            try
            {
                _userService.CreateUserCategory(userId, createCategoryDto);
                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
