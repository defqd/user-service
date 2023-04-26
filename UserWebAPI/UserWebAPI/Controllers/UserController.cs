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
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var creator = HttpContext?.User.FindFirstValue(ClaimTypes.Name);

            var existUser = await _userRepository.UserExistsAsync(user.Login);

            if (existUser)
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
        /// <summary>
        /// Метод возвращает список активных пользователей
        /// </summary>
        /// <returns>Возвращает код 200 и список активных пользователей - если обработка успешна или 500 - если произошла ошибка.</returns>
        [HttpGet("GetAllActive"), Authorize(Roles = "Admin")]
        public async Task<ActionResult> GetAllActiveUsers()
        {
            var result = await _userRepository.GetAllActiveUsersAsync();

            if(result == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Активных пользователей не существует");
            }

            return Ok(result);
        }
        /// <summary>
        /// Метод для запроса пользователя по логину
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <returns>Возвращает код 200 и информацию пользователе - если обработка успешна или 500 - если произошла ошибка.</returns>
        [HttpGet("GetUser/{login}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult> GetUserByLogin(string login)
        {
            var user =  await _userRepository.GetUserByLoginForAdminAsync(login);

            if (user == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Пользователя не существует");
            }

            return Ok(user);
        }
        /// <summary>
        /// Метод для апрос пользователя по токену (для пользователя)
        /// </summary>
        /// <returns>Возвращает код 200 и информацию пользователе - если обработка успешна или 400 - если произошла ошибка.</returns>
        [HttpGet("GetUser"), Authorize(Roles = "User")]
        public async Task<ActionResult> GetUser()
        {
            var curUser = HttpContext?.User.FindFirstValue(ClaimTypes.Name);

            var user =  await _userRepository.GetUserByLoginAsync(curUser);

            return Ok(user);
        }
        /// <summary>
        /// Метод для запроса всех пользователей старше определённого возраста
        /// </summary>
        /// <param name="age">Возраст пользователя</param>
        /// <returns>Возвращает код 200 и информацию пользователе - если обработка успешна или 500 - если произошла ошибка.</returns>
        [HttpGet("GetUserByAge/{age}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult> GetUserByAge(int age)
        {
            var users =  await _userRepository.GetUsersByAgeAsync(age);

            if (users == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Пользователи старше текущего возраста не найдены");
            }

            return Ok(users);
        }
        /// <summary>
        /// Метод для удаления пользователя по логину
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <returns>Возвращает код 200 - если обработка успешна или 500 - если произошла ошибка.</returns>
        [HttpDelete("DeleteUser/{login}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteUserByLogin(string login)
        {
            var existUser = await _userRepository.UserExistsAsync(login);

            if (!existUser)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Пользователя с таким логином не существует");
            }
            
            await _userRepository.DeleteUserAsync(login);

            return Ok("Пользователь успешно удален");
        }
        /// <summary>
        /// Метод для мягкого удаления пользователя по логину
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <returns>Возвращает код 200 - если обработка успешна или 500 - если произошла ошибка.</returns>
        [HttpPatch("DeleteUser/{login}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult> SoftDeleteUserByLogin(string login)
        {
            var existUser = await _userRepository.UserExistsAsync(login);

            if (!existUser)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Пользователя с таким логином не существует");
            }

            var revokedBy = HttpContext?.User.FindFirstValue(ClaimTypes.Name);

            await _userRepository.SoftDeleteUserAsync(login, revokedBy);

            return Ok("Пользователь успешно удален");
        }
        /// <summary>
        /// Метод для восстановления пользователя по логину после мягкого удаления
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <returns>Возвращает код 200 - если обработка успешна или 500 - если произошла ошибка.</returns>
        [HttpPatch("Recover/{login}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult> RecoverUserByLogin(string login)
        {
            var existUser = await _userRepository.UserExistsAsync(login);

            if (!existUser)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Пользователя с таким логином не существует");
            }

            await _userRepository.RecoverUserAsync(login);

            return Ok("Пользователь успешно восстановлен");
        }
        [HttpPatch("Login/{login}"), Authorize(Roles = "Admin,User")]
        public async Task<ActionResult> UpdateLogin(string login, string newLogin)
        {
            var curUser = HttpContext?.User.FindFirstValue(ClaimTypes.Name);
            var curRole = HttpContext?.User.FindFirstValue(ClaimTypes.Role);

            if (curRole != "Admin" && curUser != login)
                return Forbid();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if(login == newLogin)
                return StatusCode(StatusCodes.Status500InternalServerError, "Логин уже используется");

            await _userRepository.UpdateUserLoginAsync(login, newLogin, curUser);

            return Ok();
        }

        [HttpPatch("Password/{login}"), Authorize(Roles = "Admin,User")]
        public async Task<ActionResult> UpdatePassword(string login, string newPassword)
        {
            var curUser = HttpContext?.User.FindFirstValue(ClaimTypes.Name);
            var curRole = HttpContext?.User.FindFirstValue(ClaimTypes.Role);

            if (curRole != "Admin" && curUser != login)
                return Forbid();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _userRepository.UpdateUserPasswordAsync(login, newPassword, curUser);

            return Ok();
        }

        [HttpPatch("Update/{login}"), Authorize(Roles = "Admin,User")]
        public async Task<ActionResult> UpdateUser(string login, UpdateUserDto user)
        {
            var curUser = HttpContext?.User.FindFirstValue(ClaimTypes.Name);
            var curRole = HttpContext?.User.FindFirstValue(ClaimTypes.Role);

            if (curRole != "Admin" && curUser != login)
                return Forbid();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var changedUser = await _userRepository.GetUserByLoginAsync(login);

            if(changedUser == null)
                return StatusCode(StatusCodes.Status500InternalServerError, "Пользователя с таким логином не существует");

            changedUser.Login = login;
            changedUser.Name = user.Name ?? changedUser.Name;
            changedUser.BirthDay = user.BirthDay ?? changedUser.BirthDay;
            changedUser.Gender = user.Gender ?? changedUser.Gender;

            await _userRepository.UpdateUserAsync(changedUser, curUser);

            return Ok();
        }
    }
}