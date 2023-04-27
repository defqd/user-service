using System.ComponentModel.DataAnnotations;
using UserWebAPI.Helper.Validation;

namespace UserWebAPI.Dto
{
    /// <summary>
    /// Модель dto с базовыми параметрами для получения информации о пользователе
    /// </summary>
    public class GetUserByLoginDto
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        [LatinaAndCyrillicLetters]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        /// <summary>
        /// Пол пользователя
        /// </summary>
        [Range(0, 2, ErrorMessage = "Поле пол может содержать 0 - Женский, 1 - Мужской, 2 - Неизвестно")]
        public int Gender { get; set; }

        /// <summary>
        /// Дата рождения пользователя
        /// </summary>
        public DateTime? BirthDay { get; set; }

        /// <summary>
        /// Статус пользователя (активный/неактивный)
        /// </summary>
        public bool IsActive { get; set; }
    }
}
