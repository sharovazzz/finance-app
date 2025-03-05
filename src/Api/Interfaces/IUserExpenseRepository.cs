using PersonalFinanceApp.Models;

namespace PersonalFinanceApp.Interfaces
{
    public interface IUserExpenseRepository
    {
        Expense CreateUserExpense(int userId, int categoryId, CreateExpenseDto createExpenseDto);
        void DeleteUserExpense(int userId, int expenseId);
        List<Expense> GetUserExpenses(int userId);
        Expense ChangeExpenseCategory(int userId, int expenseId, int newCategoryId);
        Expense UpdateUserExpense(int userId, int expenseId, CreateExpenseDto createExpenseDto);

    }
}
