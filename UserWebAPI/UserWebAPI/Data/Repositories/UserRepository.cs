using Microsoft.EntityFrameworkCore;
using UserWebAPI.Data.Contexts;
using UserWebAPI.Dto;
using UserWebAPI.Helper.Hashing;
using UserWebAPI.Models;

namespace UserWebAPI.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _dbContext;
        public UserRepository(UserContext userContext)
        {
            _dbContext = userContext;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            var result = await _dbContext.AddAsync(user);

            await _dbContext.SaveChangesAsync();

            return result.Entity;
        }
        public async Task UpdateUserAsync(User user, string modifiedBy)
        {
            user.ModifiedBy = modifiedBy;
            user.ModifiedOn = DateTime.Now;

            _dbContext.Update(user);

            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateUserLoginAsync(string login, string newLogin, string modifiedBy)
        {
            var user = await _dbContext.User.FirstOrDefaultAsync(x => x.Login == login);

            user.Login = newLogin;
            user.ModifiedBy = modifiedBy;
            user.ModifiedOn = DateTime.Now;

            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateUserPasswordAsync(string login, string newPassword, string modifiedBy)
        {
            var user = await _dbContext.User.FirstOrDefaultAsync(x => x.Login == login);

            user.Password = HashingPassword.Hashing(newPassword);
            user.ModifiedBy = modifiedBy;
            user.ModifiedOn = DateTime.Now;

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(string login)
        {
            var user = await _dbContext.User.FirstOrDefaultAsync(x => x.Login == login);

            _dbContext.User.Remove(user);

            await _dbContext.SaveChangesAsync();
        }
        public async Task SoftDeleteUserAsync(string login, string revokedBy)
        {
            var user = await _dbContext.User.FirstOrDefaultAsync(x => x.Login == login);

            user.RevokedBy = revokedBy;
            user.RevokedOn = DateTime.Now;

            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetAllActiveUsersAsync()
        {
            var result = await _dbContext.User.Where(x => x.RevokedOn == null).OrderBy(x => x.CreatedOn).ToListAsync();

            return result;
        }

        public async Task<User> GetUserByLoginAndPasswordAsync(string login, string password)
        {
            var result = await _dbContext.User.FirstOrDefaultAsync(x => x.Login == login && x.Password == password);

            return result;
        }
        public async Task<GetUserByLoginDto> GetUserByLoginForAdminAsync(string login)
        {   
            var user = await _dbContext.User.FirstOrDefaultAsync(x => x.Login == login);

            return new GetUserByLoginDto
            {
                Name = user.Name,
                BirthDay = user.BirthDay,
                Gender = user.Gender,
                IsActive = user.RevokedOn == null
            };
        }

        public async Task<User> GetUserByLoginAsync(string login)
        {
            var user = await _dbContext.User.FirstOrDefaultAsync(x => x.Login == login);

            return user;
        }

        public async Task<IEnumerable<User>> GetUsersByAgeAsync(int age)
        {
            var result = await _dbContext.User.Where(x => x.BirthDay != null && (DateTime.Now.Year - x.BirthDay.Value.Year) >= age).ToListAsync();

            return result;
        }


        public async Task<bool> UserExistsAsync(string login)
        {
            var result = await _dbContext.User.FirstOrDefaultAsync(x => x.Login == login);

            if (result == null)
                return false;

            return true;
        }

        public async Task RecoverUserAsync(string login)
        {
            var user = await _dbContext.User.FirstOrDefaultAsync(x => x.Login == login);

            user.RevokedBy = null;
            user.RevokedOn = null;

            await _dbContext.SaveChangesAsync();
        }
    }
}