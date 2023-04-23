using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;
using System;
using UserWebAPI.Data.Contexts;
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

        public async Task CreateUserAsync(User user)
        {
            var result = await _dbContext.AddAsync(user);

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

            if(user != null)
            {
                user.RevokedBy = revokedBy;
                user.RevokedOn = DateTime.Now;
            }

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

        public async Task<bool> UserExistsAsync(string login)
        {
            var result = await _dbContext.User.FirstOrDefaultAsync(x => x.Login == login);

            if (result == null)
                return false;

            return true;
        }
    }
}