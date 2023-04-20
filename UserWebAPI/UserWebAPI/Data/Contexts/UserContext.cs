using Microsoft.EntityFrameworkCore;
using System.Reflection;
using UserWebAPI.Models;

namespace UserWebAPI.Data.Contexts
{
    public class UserContext : DbContext
    {
        public DbSet<User> User { get; set; }

        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}