using Luqma.Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Luqma.Infrastructure.Seeder
{
    public static class UserSeeder
    {
        public static async Task SeedAsync(UserManager<LuqmaUser> userManager)
        {
            var users = await userManager.Users.CountAsync();
            if (users <= 0)
            {
                var defaultUser = new LuqmaUser()
                {
                    UserName = "admin",
                    Email = "admin@luqma.com",
                    FirstName = "Luqma",
                    LastName = "Manager",
                    Gender = 0,
                    PhoneNumber = "+972568249300",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    BirthDate = new DateTime(2003, 2, 18)
                };
                await userManager.CreateAsync(defaultUser, "admin003+-");
                await userManager.AddToRoleAsync(defaultUser, "Manager");
            }
        }
    }
}
