using PersonalFinanceApp.Models;

namespace PersonalFinanceApp.Interfaces
{
    public interface IUserRepository
    {
        List<ShortUser> GetAllUsers();
        User GetUser(int id);
        List<User> GetUsersByEmailOrPhone(string email, string phone);
        User CreateUser(UserDto userDto);
        void UpdateUser(int id, UserDto userDto);
        void DeleteUser(int id);
        void ResetUserCategories(int id);
        void DeleteUserCategory(int userId, int categoryId);
        Category CreateUserCategory(int userId, CreateCategoryDto createCategoryDto);
        Expense CreateUserExpense(int userId, int categoryId, CreateExpenseDto createExpenseDto);
        void DeleteUserExpense(int userId, int  expenseId);
        List<Expense> GetUserExpenses(int userId);
        Expense ChangeExpenseCategory(int userId, int expenseId, int newCategoryId);
        Expense UpdateUserExpense(int userId, int expenseId, CreateExpenseDto createExpenseDto);

    }
}
