﻿using PersonalFinanceApp.Models;

namespace PersonalFinanceApp.Interfaces
{
    public interface IUserService
    {
        List<User> GetAllUsers();
        User GetUser(int id);
        User CreateUser(CreateUserDto createUserDto);
        void UpdateUser(int id, UserDto userDto);
        void DeleteUser(int id);
        void ResetUserCategories(int id);
        void DeleteUserCategory(int userId, int categoryId);
        Category CreateUserCategory(int userId, CreateCategoryDto createCategoryDto);
    }
}
