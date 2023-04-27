using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserWebAPI.Dto;
using UserWebAPI.Services;

namespace UserWebAPI.Controllers
{
    /// <summary>
    /// Класс контроллера для выполнения CRUD операций над сущностью User
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
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

            var createdUser = await _userService.CreateUserAsync(creator, user);

            return Ok(createdUser);
        }
        /// <summary>
        /// Метод возвращает список активных пользователей
        /// </summary>
        /// <returns>Возвращает код 200 и список активных пользователей - если обработка успешна или 500 - если произошла ошибка.</returns>
        [HttpGet("GetAllActive"), Authorize(Roles = "Admin")]
        public async Task<ActionResult> GetAllActiveUsers()
        {
            var users = await _userService.GetAllUsersAsync();

            return Ok(users);
        }
        /// <summary>
        /// Метод для запроса пользователя по логину
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <returns>Возвращает код 200 и информацию пользователе - если обработка успешна или 500 - если произошла ошибка.</returns>
        [HttpGet("GetUser/{login}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult> GetUserByLogin(string login)
        {
            var user = await _userService.GetUserAsync(login);

            return Ok(user);
        }
        /// <summary>
        /// Метод для запрос пользователя по токену (для пользователя)
        /// </summary>
        /// <returns>Возвращает код 200 и информацию пользователе - если обработка успешна или 500 - если произошла ошибка.</returns>
        [HttpGet("GetUser"), Authorize(Roles = "User")]
        public async Task<ActionResult> GetUser()
        {
            var curUser = HttpContext?.User.FindFirstValue(ClaimTypes.Name);

            var user = await _userService.GetCurrentUserAsync(curUser);

            return Ok(user);
        }
        /// <summary>
        /// Метод для запроса всех пользователей старше определённого возраста
        /// </summary>
        /// <param name="age">Возраст пользователя</param>
        /// <returns>Возвращает код 200 и информацию пользователе - если обработка успешна или 500 - если произошла ошибка.</returns>
        [HttpGet("GetUserByAge/{age}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult> GetUsersByAge(int age)
        {
            var users =  await _userService.GetUsersByAgeAsync(age);

            return Ok(users);
        }
        /// <summary>
        /// Метод для удаления пользователя по логину
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <param name="soft">Полное или мягкое удаление</param>
        /// <returns>Возвращает код 200 - если обработка успешна или 500 - если произошла ошибка.</returns>
        [HttpDelete("DeleteUser/{login}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteUserByLogin(string login, bool soft)
        {
            var revokedBy = HttpContext?.User.FindFirstValue(ClaimTypes.Name);

            await _userService.DeleteUserAsync(login, soft, revokedBy);  

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
            await _userService.RecoverUserAsync(login);

            return Ok("Пользователь успешно восстановлен");
        }
        /// <summary>
        /// Метод для изменения логина пользователя
        /// </summary>
        /// <param name="user">Модель dto с базовыми параметрами для изменения логина пользователя</param>
        /// <returns>Возвращает код 200 - если обработка успешна или 500 - если произошла ошибка.</returns>
        [HttpPatch("UpdateLogin"), Authorize(Roles = "Admin,User")]
        public async Task<ActionResult> UpdateLogin(UpdateUserLoginDto user)
        {
            var curUser = HttpContext?.User.FindFirstValue(ClaimTypes.Name);
            var curRole = HttpContext?.User.FindFirstValue(ClaimTypes.Role);

            if (curRole != "Admin" && curUser != user.Login)
                return Forbid();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _userService.UpdateUserLoginAsync(user, curUser, curRole);
            
            return Ok("Логин пользователя обновлен");
        }
        /// <summary>
        /// Метод для изменения пароля пользователя
        /// </summary>
        /// <param name="user">Модель dto с базовыми параметрами для изменения пароля пользователя</param>
        /// <returns>Возвращает код 200 - если обработка успешна или 500 - если произошла ошибка.</returns>
        [HttpPatch("UpdatePassword"), Authorize(Roles = "Admin,User")]
        public async Task<ActionResult> UpdatePassword(UpdateUserPasswordDto user)
        {
            var curUser = HttpContext?.User.FindFirstValue(ClaimTypes.Name);
            var curRole = HttpContext?.User.FindFirstValue(ClaimTypes.Role);

            if (curRole != "Admin" && curUser != user.Login)
                return Forbid();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _userService.UpdateUserPasswordAsync(user, curUser, curRole);

            return Ok("Пароль пользователя обновлен");
        }
        /// <summary>
        /// Метод для изменения имени, даты рождения или пола пользователя
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <param name="user">Модель dto для изменения имени, даты рождения или пола пользователя</param>
        /// <returns>Возвращает код 200 - если обработка успешна или 500 - если произошла ошибка.</returns>
        [HttpPatch("Update/{login}"), Authorize(Roles = "Admin,User")]
        public async Task<ActionResult> UpdateUser(string login, UpdateUserDto user)
        {
            var curUser = HttpContext?.User.FindFirstValue(ClaimTypes.Name);
            var curRole = HttpContext?.User.FindFirstValue(ClaimTypes.Role);

            if (curRole != "Admin" && curUser != login)
                return Forbid();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _userService.UpdateUserAsync(login, user, curUser, curRole);

            return Ok("Данные пользователя обновлены");
        }
    }
}