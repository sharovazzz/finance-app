using Microsoft.AspNetCore.Mvc;
using PersonalFinanceApp.Interfaces;
using PersonalFinanceApp.Models;

namespace PersonalFinanceApp.Controllers
{
    [Route("api/users/{userId}/expense")]
    [ApiController]
    public class UserExpenseController : Controller
    {
        private readonly IUserService _userService;

        public UserExpenseController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public IActionResult CreateUserExpense(int userId, int categoryId, [FromBody] CreateExpenseDto createExpenseDto)
        {
            try
            {
                var expense = _userService.CreateUserExpense(userId, categoryId, createExpenseDto);
                return Ok(expense);
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

        [HttpDelete("{expenseId}")]
        public IActionResult DeleteUserExpense(int userId, int expenseId)
        {
            try
            {
                _userService.DeleteUserExpense(userId, expenseId);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet]
        public IActionResult GetUserExpense(int userId)
        {
            try
            {
                var expense = _userService.GetUserExpenses(userId);
                return Ok(expense);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPut("change/{expenseId}")]
        public IActionResult ChangeExpenseCategory(int userId, int expenseId, int newCategoryId)
        {
            try
            {
                var expense = _userService.ChangeExpenseCategory(userId, expenseId, newCategoryId);
                return Ok(expense);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPut("{expenseId}")]
        public IActionResult UpdateUserExpense(int userId, int expenseId, [FromBody] CreateExpenseDto createExpenseDto)
        {
            try
            {
                var expense = _userService.UpdateUserExpense(userId, expenseId, createExpenseDto);
                return Ok(expense);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
