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
        [RegularExpression("[a-zA-Z\\d]+")]
        public string Login { get; set; }
        /// <summary>
        /// Пароль пользователя
        /// </summary>
        [RegularExpression("[a-zA-Z\\d]+")]
        public string Password { get; set; }
        /// <summary>
        /// Имя пользователя
        /// </summary>
        [RegularExpression("[a-zA-Zа-яА-ЯёЁ]+")]
        public string Name { get; set; }
        /// <summary>
        /// Пол пользователя
        /// </summary>
        [RegularExpression("[0-2]")]
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