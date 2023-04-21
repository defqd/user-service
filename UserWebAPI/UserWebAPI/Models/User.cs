using System.ComponentModel.DataAnnotations;

namespace UserWebAPI.Models
{
    /// <summary>
    /// Класс сущности User
    /// </summary>
    public class User
    {
        /// <summary>
        /// Ид. пользователя
        /// </summary>
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// Логин пользователя
        /// </summary>
        [RegularExpression("[a-zA-Z\\d]")]
        public string Login { get; set; }
        /// <summary>
        /// Пароль пользователя
        /// </summary>
        [RegularExpression("[a-zA-Z\\d]")]
        public string Password { get; set; }
        /// <summary>
        /// Имя пользователя
        /// </summary>
        [RegularExpression("[a-zA-Z\\d]")]
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
        /// <summary>
        /// Дата создания пользователя
        /// </summary>
        public DateTime? CreatedOn { get; set; }
        /// <summary>
        /// Логин Пользователя, от имени которого этот пользователь создан
        /// </summary>
        public string? CreatedBy { get; set; }
        /// <summary>
        /// Дата изменения пользователя
        /// </summary>
        public DateTime? ModifiedOn { get; set; }
        /// <summary>
        /// Логин Пользователя, от имени которого этот пользователь изменён
        /// </summary>
        public string? ModifiedBy { get; set; }
        /// <summary>
        /// Дата удаления пользователя
        /// </summary>
        public DateTime? RevokedOn { get; set; }
        /// <summary>
        /// Логин Пользователя, от имени которого этот пользователь удалён
        /// </summary>
        public string? RevokedBy { get; set; }
    }
}