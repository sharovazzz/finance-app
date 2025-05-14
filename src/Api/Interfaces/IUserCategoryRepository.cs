using PersonalFinanceApp.Models;

namespace PersonalFinanceApp.Interfaces
{
    public interface IUserCategoryRepository
    {
        void ResetUserCategories(int id);
        void ResetBudgetCategories(int userId);
        void DeleteUserCategory(int userId, int categoryId);
        Category CreateUserCategory(int userId, CreateCategoryDto createCategoryDto);
        void UpdateCategoryBudget(int userId, int categoryId, CreateCategoryBudgetDto createCategoryBudgetDto);
        void UpdateCategorySpent(int categoryId, decimal amount);
    }
}
