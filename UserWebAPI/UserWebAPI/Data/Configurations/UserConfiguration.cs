using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserWebAPI.Models;

namespace UserWebAPI.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(e => e.Id);

            builder.ToTable("User");

            builder.Property(e => e.Login).HasColumnName("Login");

            builder.Property(e => e.Password).HasColumnName("Password");

            builder.Property(e => e.Name).HasColumnName("Name");

            builder.Property(e => e.Gender).HasColumnName("Gender");

            builder.Property(e => e.BirthDay).HasColumnName("Birthday");

            builder.Property(e => e.Admin).HasColumnName("Admin");

            builder.Property(e => e.CreatedOn).HasColumnName("CreatedOn");

            builder.Property(e => e.CreatedBy).HasColumnName("CreatedBy");

            builder.Property(e => e.ModifiedOn).HasColumnName("ModifiedOn");

            builder.Property(e => e.ModifiedBy).HasColumnName("ModifiedBy");

            builder.Property(e => e.RevokedOn).HasColumnName("RevokedOn");

            builder.Property(e => e.RevokedBy).HasColumnName("RevokedBy");

            builder.HasData( new User
            {
                Id = Guid.NewGuid(),
                Login = "admin",
                Password = "admin",
                Name = "Админ",
                Gender = 2,
                BirthDay = DateTime.MinValue,
                Admin = true,
                CreatedOn = DateTime.Now.AddMonths(-2),
                CreatedBy = "GOD",
                ModifiedOn = DateTime.Now.AddMonths(-2),
                ModifiedBy = ""
            });
        }
    }
}