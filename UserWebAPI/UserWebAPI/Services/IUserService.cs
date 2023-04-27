using UserWebAPI.Dto;
using UserWebAPI.Models;

namespace UserWebAPI.Services
{
    public interface IUserService
    {
        public Task<User> CreateUserAsync(string creator, CreateUserDto user);
        public Task<IEnumerable<User>> GetAllUsersAsync();
        public Task<User> GetCurrentUserAsync(string login);
        public Task<GetUserByLoginDto> GetUserAsync(string login);
        public Task<IEnumerable<User>> GetUsersByAgeAsync(int age);
        public Task DeleteUserAsync(string login, bool soft, string revokedBy);
        public Task RecoverUserAsync(string login);
        public Task UpdateUserLoginAsync(UpdateUserLoginDto user, string curUser, string curRole);
        public Task UpdateUserPasswordAsync(UpdateUserPasswordDto user, string curUser, string curRole);
        public Task UpdateUserAsync(string login, UpdateUserDto user, string curUser, string curRole);

    }
}