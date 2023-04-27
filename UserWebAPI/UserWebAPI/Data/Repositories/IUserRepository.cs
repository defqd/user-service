using UserWebAPI.Dto;
using UserWebAPI.Models;

namespace UserWebAPI.Data.Repositories
{
    /// <summary>
    /// Интерфейс методов для работы с сущность User
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Метод для регистрация нового пользователя
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <returns></returns>
        public Task<User> CreateUserAsync(User user);
        /// <summary>
        /// Метод для проверки существования пользователя по логину 
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public Task<bool> UserExistsAsync(string login);
        /// <summary>
        /// Метод для поиска пользователя по логину и паролю
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <param name="password">Пароль пользователя</param>
        /// <returns></returns>
        public Task<User> GetUserByLoginAndPasswordAsync(string login, string password);
        /// <summary>
        /// Метод для поиска всех активных пользователей
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<User>> GetAllActiveUsersAsync();
        /// <summary>
        /// Метод для удаления пользователя по логину
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <returns></returns>
        public Task DeleteUserAsync(string login);
        /// <summary>
        /// Метод для мягкого удаления пользователя по логину
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <param name="revorkedBy">Логин Пользователя, от имени которого этот пользователь удалён</param>
        /// <returns></returns>
        public Task SoftDeleteUserAsync(string login, string revorkedBy);
        /// <summary>
        /// Метод для восстановления пользователя по логину - очистка полей (RevokedOn, RevokedBy) 
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <returns></returns>
        public Task RecoverUserAsync(string login);
        /// <summary>
        /// Метод для запроса пользователя по логину (для админа)
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <returns></returns>
        public Task<GetUserByLoginDto> GetUserByLoginForAdminAsync(string login);
        /// <summary>
        /// Метод для запроса пользователя по логину (для пользователя)
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <returns></returns>
        public Task<User> GetUserByLoginAsync(string login);
        /// <summary>
        /// Метод для запроса всех пользователей старше определённого возраста
        /// </summary>
        /// <param name="age">Возраст пользователей</param>
        /// <returns></returns>
        public Task<IEnumerable<User>> GetUsersByAgeAsync(int age);
        /// <summary>
        /// Метод для изменения логина пользователя
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <param name="newLogin">Новый логин пользоателя</param>
        /// <param name="modifiedBy">Кем изменено</param>
        /// <returns></returns>
        public Task UpdateUserLoginAsync(string login, string newLogin, string modifiedBy);
        /// <summary>
        /// Метод для изменения пароля пользователя
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <param name="newPassword">Новый пароль пользователя</param>
        /// <param name="modifiedBy">Кем изменено</param>
        /// <returns></returns>
        public Task UpdateUserPasswordAsync(string login, string newPassword, string modifiedBy);
        /// <summary>
        /// Метод для изменения имени, даты рождения или пола пользователя
        /// </summary>
        /// <param name="user">Логин пользователя</param>
        /// <param name="modifiedBy">Кем изменено</param>
        /// <returns></returns>
        public Task UpdateUserAsync(User user, string modifiedBy);
    }
}