using PersonalFinanceApp.Models;

namespace PersonalFinanceApp.Interfaces
{
    public interface IUserBudgetService
    {
        Budget CreateUserBudget(int userId, CreateBudgetDto createBudgetDto);
        Budget GetNowBudget(int userId);
        List<Budget> GetUserBudgets(int userId);
    }
}
