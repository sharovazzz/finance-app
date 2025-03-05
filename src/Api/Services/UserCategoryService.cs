using PersonalFinanceApp.Interfaces;
using PersonalFinanceApp.Models;

namespace PersonalFinanceApp.Services
{
    public class UserCategoryService : IUserCategoryService
    {
        private readonly IUserCategoryRepository _userCategoryRepository;
        private readonly IUserRepository _userRepository;

        public UserCategoryService(IUserCategoryRepository userCategoryRepository, IUserRepository userRepository)
        {
            _userCategoryRepository = userCategoryRepository;
            _userRepository = userRepository;
        }
        public void ResetUserCategories(int id)
        {
            var user = _userRepository.GetUser(id);

            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            _userCategoryRepository.ResetUserCategories(id);
        }

        public void DeleteUserCategory(int userId, int categoryId)
        {
            var user = _userRepository.GetUser(userId);
            var category = user.Categories.FirstOrDefault(c => c.Id == categoryId);

            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            if (category == null)
            {
                throw new KeyNotFoundException("Category not found");
            }

            _userCategoryRepository.DeleteUserCategory(userId, categoryId);
        }

        public Category CreateUserCategory(int userId, CreateCategoryDto createCategoryDto)
        {
            var user = _userRepository.GetUser(userId);

            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            return _userCategoryRepository.CreateUserCategory(userId, createCategoryDto);
        }
    }
}
