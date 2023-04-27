
using System.ComponentModel.DataAnnotations;
using UserWebAPI.Helper.Validation;

namespace UserWebAPI.Dto
{
    /// <summary>
    /// Модель dto с базовыми параметрами для изменения пароля пользователя
    /// </summary>
    public class UpdateUserPasswordDto
    {
        /// <summary>
        /// Логин пользователя
        /// </summary>
        [LatinaLettersAndNumbersAttribute]
        [StringLength(50, MinimumLength = 3)]
        public string Login { get; set; }

        /// <summary>
        /// Пароль пользователя
        /// </summary>
        [LatinaLettersAndNumbersAttribute]
        [StringLength(50, MinimumLength = 6)]
        public string NewPassword { get; set; }
    }
}