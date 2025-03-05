using PersonalFinanceApp.Interfaces;
using PersonalFinanceApp.Models;
using PersonalFinanceApp.Repository;

namespace PersonalFinanceApp.Services
{
    public class UserExpenseService : IUserExpenseService
    {
        private readonly IUserExpenseRepository _userExpenseRepository;
        private readonly IUserRepository _userRepository;

        public UserExpenseService(IUserExpenseRepository userExpenseRepository, IUserRepository userRepository)
        {
            _userExpenseRepository = userExpenseRepository;
            _userRepository = userRepository;
        }


        public Expense CreateUserExpense(int userId, int categoryId, CreateExpenseDto createExpenseDto)
        {
            var user = _userRepository.GetUser(userId);

            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            var category = user.Categories.FirstOrDefault(c => c.Id == categoryId);

            if (category == null)
            {
                throw new KeyNotFoundException("Category not found");
            }

            if (createExpenseDto.Amount <= 0)
            {
                throw new ArgumentException("Incorrect expense amount.");
            }

            return _userExpenseRepository.CreateUserExpense(userId, categoryId, createExpenseDto);
        }

        public void DeleteUserExpense(int userId, int expenseId)
        {
            var user = _userRepository.GetUser(userId);

            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            var expense = user.Expenses.FirstOrDefault(e => e.Id == expenseId);

            if (expense == null)
            {
                throw new KeyNotFoundException("Expense not found");
            }

            _userExpenseRepository.DeleteUserExpense(userId, expenseId);
        }

        public List<Expense> GetUserExpenses(int userId)
        {
            var user = _userRepository.GetUser(userId);

            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            return _userExpenseRepository.GetUserExpenses(userId);
        }

        public Expense ChangeExpenseCategory(int userId, int expenseId, int newCategoryId)
        {
            var user = _userRepository.GetUser(userId);

            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            var expense = user.Expenses.FirstOrDefault(e => e.Id == expenseId);

            if (expense == null)
            {
                throw new KeyNotFoundException("Expense not found");
            }

            var category = user.Categories.FirstOrDefault(c => c.Id == newCategoryId);

            if (category == null)
            {
                throw new KeyNotFoundException("Category not found");
            }

            return _userExpenseRepository.ChangeExpenseCategory(userId, expenseId, newCategoryId);
        }

        public Expense UpdateUserExpense(int userId, int expenseId, CreateExpenseDto createExpenseDto)
        {
            var user = _userRepository.GetUser(userId);

            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            var expense = user.Expenses.FirstOrDefault(e => e.Id == expenseId);

            if (expense == null)
            {
                throw new KeyNotFoundException("Expense not found");
            }

            if (createExpenseDto.Amount <= 0)
            {
                throw new ArgumentException("Incorrect expense amount.");
            }

            return _userExpenseRepository.UpdateUserExpense(userId, expenseId, createExpenseDto);
        }
    }
}
