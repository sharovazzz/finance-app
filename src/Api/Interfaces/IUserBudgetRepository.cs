using PersonalFinanceApp.Models;

namespace PersonalFinanceApp.Interfaces
{
    public interface IUserBudgetRepository
    {
        Budget CreateUserBudget(int userId, CreateBudgetDto createBudgetDto);
        Budget GetNowBudget(int userId);
        List <Budget> GetUserBudgets(int userId);
        void UpdateBudget(Budget budget);
    }
}
