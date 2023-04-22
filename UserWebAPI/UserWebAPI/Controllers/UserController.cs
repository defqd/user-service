using Microsoft.AspNetCore.Mvc;
using UserWebAPI.Data.Repositories;
using UserWebAPI.Dto;
using UserWebAPI.Models;

namespace UserWebAPI.Controllers
{
    /// <summary>
    /// Класс контроллера для выполнения CRUD операций над сущностью User
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Метод для создание пользователя по логину, паролю, имени, полу и дате рождения + указание будет ли пользователь админом
        /// </summary>
        /// <param name="login">Логмн пользователя</param>
        /// <param name="password">Пароль пользователя</param>
        /// <param name="user">Модель dto для регистрации пользователя</param>
        /// <returns>Возвращает код 200 - если обработка успешна или 500 - если произошла ошибка.</returns>
        [HttpPost("create")]
        public async Task<ActionResult> CreateUserAsync(string login, string password, CreateUserDto user)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var existUser = await _userRepository.UserExistsAsync(login, password);

                if(existUser == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                    "Неправильный логин или пароль");
                }

                if (!existUser.Admin)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                    "Это действие доступно только администратору");
                }

                var newUser = new User
                {
                    Login = user.Login,
                    Password = user.Password,
                    Name = user.Name,
                    Gender = user.Gender,
                    BirthDay = user.BirthDay,
                    Admin = user.Admin,
                    CreatedBy = user.Login,
                    CreatedOn = DateTime.Now
                };

                await _userRepository.CreateUserAsync(newUser);

                return Ok("Пользователь успешно добавлен");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ошибка при создании нового пользователя");
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetAllActiveUsers(string login, string password)
        {
            var existUser = await _userRepository.UserExistsAsync(login, password);

            if (existUser == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Неправильный логин или пароль");
            }

            if (!existUser.Admin)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Это действие доступно только администратору");
            }

            var result = await _userRepository.GetAllActiveUsersAsync();

            return Ok(result);
        }
    }
}