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

        public async Task<User> UserExistsAsync(string login, string password)
        {
            var result = await _dbContext.User.FirstOrDefaultAsync(x => x.Login == login && x.Password == password);

            return result;
        }
    }
}