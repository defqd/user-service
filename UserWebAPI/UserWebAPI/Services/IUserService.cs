using UserWebAPI.Dto;
using UserWebAPI.Models;

namespace UserWebAPI.Services
{
    /// <summary>
    /// Интерфейс методов для работы с сущность User
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Метод для регистрация нового пользователя
        /// </summary>
        /// <param name="creator">Логин Пользователя, от имени которого этот пользователь создан</param>
        /// <param name="user">Модель dto для регистрации пользователя</param>
        /// <returns></returns>
        public Task<User> CreateUserAsync(string creator, CreateUserDto user);

        /// <summary>
        /// Метод для поиска всех активных пользователей
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<User>> GetAllUsersAsync();

        /// <summary>
        /// Метод для запроса пользователя по логину (для пользователя)
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <returns></returns>
        public Task<User> GetCurrentUserAsync(string login);

        /// <summary>
        /// Метод для запроса пользователя по логину
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <returns></returns>
        public Task<GetUserByLoginDto> GetUserAsync(string login);

        /// <summary>
        /// Метод для запроса всех пользователей старше определённого возраста
        /// </summary>
        /// <param name="age">Возраст пользователей</param>
        /// <returns></returns>
        public Task<IEnumerable<User>> GetUsersByAgeAsync(int age);

        /// <summary>
        /// Метод для полного или мягкого удаления пользователя по логину
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <param name="soft">Полное или мягкое удаления пользователя</param>
        /// <param name="revokedBy">Логин Пользователя, от имени которого этот пользователь удалён</param>
        /// <returns></returns>
        public Task DeleteUserAsync(string login, bool soft, string revokedBy);

        /// <summary>
        /// Восстановление пользователя
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <returns></returns>
        public Task RecoverUserAsync(string login);

        /// <summary>
        /// Метод для изменения логина пользователя
        /// </summary>
        /// <param name="user">Модель dto с базовыми параметрами для изменения логина пользователя</param>
        /// <param name="curUser">Логин текущего пользователя</param>
        /// <returns></returns>
        public Task UpdateUserLoginAsync(UpdateUserLoginDto user, string curUser);

        /// <summary>
        /// Метод для изменения пароля пользователя
        /// </summary>
        /// <param name="user">Модель dto с базовыми параметрами для изменения пароля пользователя</param>
        /// <param name="curUser">Логин текущего пользователя</param>
        /// <returns></returns>
        public Task UpdateUserPasswordAsync(UpdateUserPasswordDto user, string curUser);

        /// <summary>
        /// Метод для изменения имени, даты рождения или пола пользователя
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <param name="user">Модель dto для изменения имени, даты рождения или пола пользователя</param>
        /// <param name="curUser">Текущий пользователь</param>
        /// <returns></returns>
        public Task UpdateUserAsync(string login, UpdateUserDto user, string curUser);

    }
}