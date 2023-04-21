using System.ComponentModel.DataAnnotations;

namespace UserWebAPI.Dto
{
    /// <summary>
    /// Модель dto с базовыми параметрами для регистрации нового пользователя
    /// </summary>
    public class CreateUserDto
    {
        /// <summary>
        /// Логи пользователя
        /// </summary>
        [StringLength(50, MinimumLength = 3)]
        public string Login { get; set; }
        /// <summary>
        /// Пароль пользователя
        /// </summary>
        [StringLength(50, MinimumLength = 6)]
        public string Password { get; set; }
        /// <summary>
        /// Имя пользователя
        /// </summary>
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }
        /// <summary>
        /// Пол пользователя
        /// </summary>
        public int Gender { get; set; }
        /// <summary>
        /// Дата рождения пользователя
        /// </summary>
        public DateTime? BirthDay { get; set; }
        /// <summary>
        /// Является ли пользователь админом
        /// </summary>
        public bool Admin { get; set; }
    }
}