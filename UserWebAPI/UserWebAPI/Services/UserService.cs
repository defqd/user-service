using UserWebAPI.Data.Repositories;
using UserWebAPI.Dto;
using UserWebAPI.Models;

namespace UserWebAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> CreateUserAsync(string creator, CreateUserDto user)
        {
            var existUser = await _userRepository.UserExistsAsync(user.Login);

            if (existUser)
                throw new ArgumentException("Пользователь с таким логином уже существует");

            User newUser = new()
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

            var result = await _userRepository.CreateUserAsync(newUser);

            return result;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllActiveUsersAsync();

            if (users == null)
                throw new ArgumentException("Активных пользователей не существует");

            return users;
        }

        public async Task<User> GetCurrentUserAsync(string login)
        {
            var existUser = await _userRepository.UserExistsAsync(login);

            if (!existUser)
                throw new ArgumentException("Пользователя с таким логином не существует");

            var user = await _userRepository.GetUserByLoginAsync(login);

            return user;
        }

        public async Task<GetUserByLoginDto> GetUserAsync(string login)
        {
            var existUser = await _userRepository.UserExistsAsync(login);

            if (!existUser)
                throw new ArgumentException("Пользователя с таким логином не существует");

            var user = await _userRepository.GetUserByLoginForAdminAsync(login);

            return user;
        }

        public async Task<IEnumerable<User>> GetUsersByAgeAsync(int age)
        {
            var users = await _userRepository.GetUsersByAgeAsync(age);

            if (users == null)
                throw new ArgumentException("Пользователи старше текущего возраста не найдены");

            return users;
        }
        public async Task DeleteUserAsync(string login, bool soft, string revokedBy)
        {
            var existUser = await _userRepository.UserExistsAsync(login);

            if (!existUser)
                throw new ArgumentException("Пользователя с таким логином не существует");

            if (soft)
                await _userRepository.SoftDeleteUserAsync(login, revokedBy);
            else
                await _userRepository.DeleteUserAsync(login);
        }

        public async Task RecoverUserAsync(string login)
        {
            var existUser = await _userRepository.UserExistsAsync(login);

            if (!existUser)
                throw new ArgumentException("Пользователя с таким логином не существует");

            await _userRepository.RecoverUserAsync(login);
        }

        public async Task UpdateUserLoginAsync(UpdateUserLoginDto user, string curUser, string curRole)
        {
            if (user.Login == user.NewLogin)
                throw new ArgumentException("Логин уже используется");

            var existUser = await _userRepository.UserExistsAsync(user.Login);

            if (!existUser)
                throw new ArgumentException("Пользователя с таким логином не существует");

            await _userRepository.UpdateUserLoginAsync(user.Login, user.NewLogin, curUser);
        }

        public async Task UpdateUserPasswordAsync(UpdateUserPasswordDto user, string curUser, string curRole)
        {
            var existUser = await _userRepository.UserExistsAsync(user.Login);

            if (!existUser)
                throw new ArgumentException("Пользователя с таким логином не существует");

            await _userRepository.UpdateUserPasswordAsync(user.Login, user.NewPassword, curUser);
        }

        public async Task UpdateUserAsync(string login, UpdateUserDto user, string curUser, string curRole)
        {
            var changedUser = await _userRepository.GetUserByLoginAsync(login);

            if (changedUser == null)
                throw new ArgumentException("Пользователя с таким логином не существует");

            changedUser.Login = login;
            changedUser.Name = user.Name ?? changedUser.Name;
            changedUser.BirthDay = user.BirthDay ?? changedUser.BirthDay;
            changedUser.Gender = user.Gender ?? changedUser.Gender;

            await _userRepository.UpdateUserAsync(changedUser, curUser);
        }
    }
}