﻿using UserWebAPI.Models;

namespace UserWebAPI.Data.Repositories
{
    /// <summary>
    /// Интерфейс методов для работы с сущность User
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Регистрация нового пользователя
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <returns></returns>
        public Task<User> CreateUserAsync(User user);
        /// <summary>
        /// Поиск пользователя по логину
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <returns></returns>
        public Task<User> UserExistsAsync(string login, string password);
    }
}