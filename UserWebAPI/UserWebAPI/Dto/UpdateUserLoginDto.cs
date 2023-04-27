using System.ComponentModel.DataAnnotations;
using UserWebAPI.Helper.Validation;

namespace UserWebAPI.Dto
{
    /// <summary>
    /// Модель dto с базовыми параметрами для изменения логина пользователя
    /// </summary>
    public class UpdateUserLoginDto
    {
        /// <summary>
        /// Логин пользователя
        /// </summary>
        [LatinaLettersAndNumbersAttribute]
        [StringLength(50, MinimumLength = 3)]
        public string Login { get; set; }
        /// <summary>
        /// Новый логин пользователя
        /// </summary>
        [LatinaLettersAndNumbersAttribute]
        [StringLength(50, MinimumLength = 3)]
        public string NewLogin { get; set; }
    }
}