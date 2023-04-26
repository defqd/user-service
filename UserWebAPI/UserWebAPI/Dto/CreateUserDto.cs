using System.ComponentModel.DataAnnotations;
using UserWebAPI.Helper.Validation;

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
        [LatinaLettersAndNumbersAttribute]
        [StringLength(50, MinimumLength = 3)]
        public string Login { get; set; }
        /// <summary>
        /// Пароль пользователя
        /// </summary>
        [LatinaLettersAndNumbersAttribute]
        [StringLength(50, MinimumLength = 6)]
        public string Password { get; set; }
        /// <summary>
        /// Имя пользователя
        /// </summary>
        [LatinaAndCyrillicLettersAttribute]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }
        /// <summary>
        /// Пол пользователя
        /// </summary>
        [Range(0, 2, ErrorMessage = "Поле пол может содержать 0 - Женский, 1 - Мужской, 2 - Неизвестно")]
        public int Gender { get; set; } = 2;
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