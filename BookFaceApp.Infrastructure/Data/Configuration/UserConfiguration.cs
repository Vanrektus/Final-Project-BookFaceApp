using BookFaceApp.Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookFaceApp.Infrastructure.Data.Configuration
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(SeedUsers());
        }

        private List<User> SeedUsers()
        {
            var users = new List<User>();
            var hasher = new PasswordHasher<User>();

            var user = new User()
            {
                Id = "dea12856-c198-4129-b3f3-b893d8395082",
                UserName = "Admin",
                NormalizedUserName = "ADMIN",
                FirstName = "Vancho",
                LastName = "Vanchov",
                Email = "admin@mail.com",
                NormalizedEmail = "ADMIN@MAIL.COM"
            };

            user.PasswordHash =
                 hasher.HashPassword(user, "admin123");

            users.Add(user);

           
            user = new User()
            {
                Id = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                UserName = "Guest",
                NormalizedUserName = "GUEST",
                FirstName = "Gostin",
                LastName = "Gostinov",
                Email = "guest@mail.com",
                NormalizedEmail = "GUEST@MAIL.COM"
            };

            user.PasswordHash =
            hasher.HashPassword(user, "guest123");

            users.Add(user);

            return users;
        }
    }
}
