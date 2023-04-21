using Microsoft.EntityFrameworkCore;
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

        public async Task<User> CreateUserAsync(User user)
        {
            var result = await _dbContext.AddAsync(user);

            await _dbContext.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<bool> IsAdmin(string login)
        {
            var result = await _dbContext.User.FirstOrDefaultAsync(x => x.Login == login);

            if (!result.Admin)
                return false;

            return true;
        }

        public async Task<bool> UserExistsAsync(string login)
        {
            var result = await _dbContext.User.FirstOrDefaultAsync(x => x.Login == login);
            if (result == null)
            {
                return false;
            }
            return true;
        }
    }
}