using AsistLabTask.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AsistLabTask.Data
{
    public static class UserSeed
    {
        public static void Seed(ModelBuilder builder)
        {
            var hasher = new PasswordHasher<User>();
            var user1 = new User
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                UserName = "user1@guestmail.com",
                NormalizedUserName = "USER1@GUESTMAIL.COM",
                Email = "user1@guestmail.com",
                NormalizedEmail = "USER1@GUESTMAIL.COM",
                EmailConfirmed = true,
                RegisteredAt = DateTimeOffset.UtcNow
            };
            user1.PasswordHash = hasher.HashPassword(user1, "Password123!");

            var user2 = new User
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                UserName = "user2@guestmail.com",
                NormalizedUserName = "USER2@GUESTMAIL.COM",
                Email = "user2@guestmail.com",
                NormalizedEmail = "USER2@GUESTMAIL.COM",
                EmailConfirmed = true,
                RegisteredAt = DateTimeOffset.UtcNow
            };
            user2.PasswordHash = hasher.HashPassword(user2, "Password123!");

            var user3 = new User
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                UserName = "user3@guestmail.com",
                NormalizedUserName = "USER3@GUESTMAIL.COM",
                Email = "user3@guestmail.com",
                NormalizedEmail = "USER3@GUESTMAIL.COM",
                EmailConfirmed = true,
                RegisteredAt = DateTimeOffset.UtcNow
            };
            user3.PasswordHash = hasher.HashPassword(user3, "Password123!");

            builder.Entity<User>().HasData(user1, user2, user3);
        }
    }
}
