using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalFinanceApp.Interfaces;
using PersonalFinanceApp.Models;

namespace PersonalFinanceApp.Controllers
{
    [Route("api/users/{userId}/budget")]
    [ApiController]
    public class UserBudgetController : Controller
    {
        private readonly IUserBudgetService _userBudgetService;
        public UserBudgetController(IUserBudgetService userBudgetService)
        {
            _userBudgetService = userBudgetService;
        }

        [HttpPost]
        public IActionResult CreateUserBudget(int userId, [FromBody] CreateBudgetDto createBudgetDto)
        {
            try
            {
                var budget = _userBudgetService.CreateUserBudget(userId, createBudgetDto);
                return Ok(budget);
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

        [HttpGet("now")]
        public IActionResult GetNowBudget(int userId)
        {
            var budget = _userBudgetService.GetNowBudget(userId);
            
            if (budget == null)
            {
                return NotFound();
            }

            return Ok(budget);
        }

        [HttpGet]
        public IActionResult GetUserBudgets(int userId)
        {
            var budgets = _userBudgetService.GetUserBudgets(userId);
            return Ok(budgets);
        }
    }
}
