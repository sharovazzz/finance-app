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
        private readonly IUserCategoryService _userCategoryService;
        public UserCategoryController(IUserCategoryService userCategoryService)
        {
            _userCategoryService = userCategoryService;
        }

        [HttpPut("reset")]
        public IActionResult ResetUserCategories(int userId)
        {
            try
            {
                _userCategoryService.ResetUserCategories(userId);
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
                _userCategoryService.DeleteUserCategory(userId, categoryId);
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
                var category = _userCategoryService.CreateUserCategory(userId, createCategoryDto);
                return Ok(category);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPut("{categoryId}/budget")]
        public IActionResult UpdateCategoryBudget(int userId, int categoryId, [FromBody] CreateCategoryBudgetDto createCategoryBudgetDto)
        {
            try
            {
                _userCategoryService.UpdateCategoryBudget(userId, categoryId, createCategoryBudgetDto);
                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
