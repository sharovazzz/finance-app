using Microsoft.AspNetCore.Mvc;
using PersonalFinanceApp.Interfaces;
using PersonalFinanceApp.Models;

namespace PersonalFinanceApp.Controllers
{
    [Route("api/users/{userId}/expense")]
    [ApiController]
    public class UserExpenseController : Controller
    {
        private readonly IUserExpenseService _userExpenseService;

        public UserExpenseController(IUserExpenseService userExpenseService)
        {
            _userExpenseService = userExpenseService;
        }

        [HttpPost]
        public IActionResult CreateUserExpense(int userId, int categoryId, [FromBody] CreateExpenseDto createExpenseDto)
        {
            try
            {
                var expense = _userExpenseService.CreateUserExpense(userId, categoryId, createExpenseDto);
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
                _userExpenseService.DeleteUserExpense(userId, expenseId);
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
                var expense = _userExpenseService.GetUserExpenses(userId);
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
                var expense = _userExpenseService.ChangeExpenseCategory(userId, expenseId, newCategoryId);
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
                var expense = _userExpenseService.UpdateUserExpense(userId, expenseId, createExpenseDto);
                return Ok(expense);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
