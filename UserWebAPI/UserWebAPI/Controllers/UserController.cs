using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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
        /// <param name="user">Модель dto для регистрации пользователя</param>
        /// <returns>Возвращает код 200 - если обработка успешна или 500 - если произошла ошибка.</returns>
        [HttpPost("Create"), Authorize(Roles = "Admin")]
        public async Task<ActionResult> CreateUserAsync(CreateUserDto user)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var creator = HttpContext?.User.FindFirstValue(ClaimTypes.Name);

                var existUser = _userRepository.UserExistsAsync(user.Login, user.Password);

                if(existUser == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Пользователь с таким логином уже существует");
                }

                var newUser = new User
                {
                    Login = user.Login,
                    Password = user.Password,
                    Name = user.Name,
                    Gender = user.Gender,
                    BirthDay = user.BirthDay,
                    Admin = user.Admin,
                    CreatedBy = creator,
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
        /// <summary>
        /// Метод возвращает список активных пользователей
        /// </summary>
        /// <returns>Возвращает код 200 и список активных пользователей - если обработка успешна или 500 - если произошла ошибка.</returns>
        [HttpGet("GetAllActiveUsers"), Authorize(Roles = "Admin")]
        public async Task<ActionResult> GetAllActiveUsers()
        {
            var result = await _userRepository.GetAllActiveUsersAsync();

            if(result == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Активных пользователей не существует");
            }

            return Ok(result);
        }
    }
}