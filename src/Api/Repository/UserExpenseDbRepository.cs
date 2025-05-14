using Microsoft.EntityFrameworkCore;
using PersonalFinanceApp.Data;
using PersonalFinanceApp.Interfaces;
using PersonalFinanceApp.Models;

namespace PersonalFinanceApp.Repository
{
    public class UserExpenseDbRepository : IUserExpenseRepository
    {
        private readonly ApplicationDbContext _context;

        public UserExpenseDbRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Expense CreateUserExpense(int userId, int categoryId, CreateExpenseDto createExpenseDto)
        {
            var user = _context.Users.Include(u => u.Expenses).FirstOrDefault(u => u.Id == userId);

            var expense = new Expense
            {
                Amount = createExpenseDto.Amount,
                CategoryId = categoryId,
                Date = createExpenseDto.Date,
                Description = createExpenseDto.Description
            };

            user.Expenses.Add(expense);
            _context.SaveChanges();

            return expense;
        }

        public void DeleteUserExpense(int userId, int expenseId)
        {
            var expense = _context.Expenses.FirstOrDefault(e => e.Id == expenseId);

            _context.Expenses.Remove(expense);
            _context.SaveChanges();
        }

        public List<Expense> GetUserExpenses(int userId)
        {
            var user = _context.Users
                .Include(e => e.Expenses)
                .FirstOrDefault(u => u.Id == userId);

            return user.Expenses;
        }

        public Expense ChangeExpenseCategory(int userId, int expenseId, int newCategoryId)
        {
            var expense = _context.Expenses.FirstOrDefault(e => e.Id == expenseId);

            expense.CategoryId = newCategoryId;

            _context.SaveChanges();
            return expense;
        }

        public Expense UpdateUserExpense(int userId, int expenseId, CreateExpenseDto createExpenseDto)
        {
            var expense = _context.Expenses.FirstOrDefault(e => e.Id == expenseId);

            expense.Amount = createExpenseDto.Amount;
            expense.Date = createExpenseDto.Date;
            expense.Description = createExpenseDto.Description;

            _context.SaveChanges();
            return expense;
        }
    }
}
