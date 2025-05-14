using PersonalFinanceApp.Interfaces;
using PersonalFinanceApp.Models;

namespace PersonalFinanceApp.Services
{
    public class UserBudgetService : IUserBudgetService
    {
        private readonly IUserBudgetRepository _userBudgetRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserCategoryRepository _userCategoryRepository;

        public UserBudgetService(IUserBudgetRepository userBudgetRepository, IUserRepository userRepository, IUserCategoryRepository userCategoryRepository)
        {
            _userBudgetRepository = userBudgetRepository;
            _userRepository = userRepository;
            _userCategoryRepository = userCategoryRepository;
        }

        public Budget CreateUserBudget(int userId, CreateBudgetDto createBudgetDto)
        {
            var user = _userRepository.GetUser(userId);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            if (createBudgetDto.Amount <= 0)
            {
                throw new ArgumentException("Budget amount must be greater than zero.");
            }

            if (createBudgetDto.DateStart >= createBudgetDto.DateEnd)
            {
                throw new ArgumentException("End date must be after start date.");
            }
            _userCategoryRepository.ResetBudgetCategories(userId);

            return _userBudgetRepository.CreateUserBudget(userId, createBudgetDto);
        }

        public Budget GetNowBudget(int userId)
        {
            var user = _userRepository.GetUser(userId);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            return _userBudgetRepository.GetNowBudget(userId);
        }

        public List<Budget> GetUserBudgets(int userId)
        {
            var user = _userRepository.GetUser(userId);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            return _userBudgetRepository.GetUserBudgets(userId);
        }
    }
}
