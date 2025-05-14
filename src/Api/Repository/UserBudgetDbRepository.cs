using Microsoft.EntityFrameworkCore;
using PersonalFinanceApp.Data;
using PersonalFinanceApp.Interfaces;
using PersonalFinanceApp.Models;

namespace PersonalFinanceApp.Repository
{
    public class UserBudgetDbRepository : IUserBudgetRepository
    {
        private readonly ApplicationDbContext _context;

        public UserBudgetDbRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Budget CreateUserBudget(int userId, CreateBudgetDto createBudgetDto)
        {
            var user = _context.Users.Include(u => u.Budgets).FirstOrDefault(u => u.Id == userId);

            var budget = new Budget
            {
                Amount = createBudgetDto.Amount,
                DateStart = createBudgetDto.DateStart,
                DateEnd = createBudgetDto.DateEnd,
                TotalSpend = 0,
                RemainsBudget = createBudgetDto.Amount
            };

            user.Budgets.Add(budget);
            _context.SaveChanges();

            return budget;
        }

        public Budget GetNowBudget(int userId)
        {
            var user = _context.Users
                .Include(e => e.Budgets)
                .FirstOrDefault(u => u.Id == userId);

            return user.Budgets
                .OrderByDescending(b => b.Id)
                .FirstOrDefault();
        }

        public List<Budget> GetUserBudgets(int userId)
        {
            var user = _context.Users
                .Include(e => e.Budgets)
                .FirstOrDefault(u => u.Id == userId);

            return user.Budgets;
        }

        public void UpdateBudget(Budget budget)
        {
            _context.Budgets.Update(budget);
            _context.SaveChanges();
        }
    }
}
