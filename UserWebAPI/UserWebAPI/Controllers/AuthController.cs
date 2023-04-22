using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserWebAPI.Data.Repositories;
using UserWebAPI.Dto;

namespace UserWebAPI.Controllers
{
    /// <summary>
    /// Класс контроллер для авторизации пользователя
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthController(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }
        /// <summary>
        /// Метод для получения токена
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Возвращает код 200 и токен - если обработка успешна или 500 - если произошла ошибка.</returns>
        [HttpPost("Login")]
        public async Task<ActionResult> Login(LoginUserDto user)
        {
            var existUser = await _userRepository.UserExistsAsync(user.Login, user.Password);

            if (existUser == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,"Неправильный логин или пароль");
            }

            if (existUser.RevokedBy != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Пользователь удален");
            }

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Secret").Value!));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(
                claims: new List<Claim>
                {
                    new (ClaimTypes.Name, user.Login),
                    new (ClaimTypes.Role, existUser.Admin ? "Admin":"User")
                },
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return Ok(new { Token = tokenString });
        }
    }
}