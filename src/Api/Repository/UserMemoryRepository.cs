using PersonalFinanceApp.Interfaces;
using PersonalFinanceApp.Models;

namespace PersonalFinanceApp.Repository
{
    public class UserMemoryRepository : IUserRepository
    {
        private readonly List<User> _users = new List<User>();
        private int _lastUserId = 1;
        private int _lastCategoryId = 5;
        private int _lastExpenseId = 1;

        public List<ShortUser> GetAllUsers()
        {
            return _users
                .Select(u => new ShortUser
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    Phone = u.Phone
                })
                .ToList();
        }

        public User GetUser(int id)
        {
            return _users.FirstOrDefault(u => u.Id == id);
        }

        public List<User> GetUsersByEmailOrPhone(string email, string phone)
        {
            return _users.Where(u => 
                (!string.IsNullOrEmpty(email) && u.Email == email) || 
                (!string.IsNullOrEmpty(phone) && u.Phone == phone))
                .ToList();
        }

        public User CreateUser(UserDto userDto)
        {
            var user = new User
            {
                Id = _lastUserId++,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email.Trim().ToLowerInvariant(),
                Phone = userDto.Phone,
                Categories = DefaultCategory.GetDefaultCategories(),
                Expenses = [],
                Budgets = []
            };

            _users.Add(user);
            return user;
        }

        public void UpdateUser(int id, UserDto userDto)
        {
            var userToUpdate = _users.FirstOrDefault(u => u.Id == id);

            userToUpdate.FirstName = userDto.FirstName;
            userToUpdate.LastName = userDto.LastName;
            userToUpdate.Email = userDto.Email.Trim().ToLowerInvariant();
            userToUpdate.Phone = userDto.Phone;
        }

        public void DeleteUser(int id)
        {
            var userToDelete = _users.FirstOrDefault(u => u.Id == id);

            _users.Remove(userToDelete);
        }

        public void ResetUserCategories(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);

            user.Categories = DefaultCategory.GetDefaultCategories();
        }

        public void DeleteUserCategory(int userId, int categoryId)
        {
            var user = _users.FirstOrDefault(u => u.Id == userId);

            var category = user.Categories.FirstOrDefault(c => c.Id == categoryId);

            user.Categories.Remove(category);
        }

        public Category CreateUserCategory(int userId, CreateCategoryDto createCategoryDto)
        {
            var user = _users.FirstOrDefault(u => u.Id == userId);

            var category = new Category
            {
                Id = _lastCategoryId++,
                Name = createCategoryDto.Name
            };

            user.Categories.Add(category);
            return category;
        }

        public Expense CreateUserExpense(int userId, int categoryId, CreateExpenseDto createExpenseDto)
        {
            var user = _users.FirstOrDefault(u => u.Id == userId);

            var expense = new Expense
            {
                Id = _lastExpenseId++,
                Amount = createExpenseDto.Amount,
                CategoryId = categoryId,
                Date = createExpenseDto.Date,
                Description = createExpenseDto.Description
            };

            user.Expenses.Add(expense);
            return expense;
        }

        public void DeleteUserExpense(int userId, int expenseId)
        {
            var user = _users.FirstOrDefault(u => u.Id == userId);

            var expense = user.Expenses.FirstOrDefault(e => e.Id == expenseId);

            user.Expenses.Remove(expense);
        }

        public List<Expense> GetUserExpenses(int userId)
        {
            var user = _users.FirstOrDefault(u => u.Id == userId);

            return user.Expenses;
        }

        public Expense ChangeExpenseCategory(int userId, int expenseId, int newCategoryId)
        {
            var user = _users.FirstOrDefault(e => e.Id == userId);

            var expense = user.Expenses.FirstOrDefault(e => e.Id == expenseId);

            expense.CategoryId = newCategoryId;

            return expense;
        }

        public Expense UpdateUserExpense(int userId, int expenseId, CreateExpenseDto createExpenseDto)
        {
            var user = _users.FirstOrDefault(u => u.Id == userId);

            var expense = user.Expenses.FirstOrDefault(e => e.Id == expenseId);

            expense.Amount = createExpenseDto.Amount;
            expense.Date = createExpenseDto.Date;
            expense.Description = createExpenseDto.Description;

            return expense;
        }
    }
}
