using Luqma.Data.Entities.Identity;
using Luqma.Data.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Luqma.Infrastructure.Seeder
{
    public static class RoleSeeder
    {
        public static async Task SeedAsync(RoleManager<LuqmaRole> roleManager)
        {
            var roles = await roleManager.Roles.CountAsync();
            if (roles <= 0)
            {
                await roleManager.CreateAsync(new LuqmaRole() { Name = Roles.Manager });
                await roleManager.CreateAsync(new LuqmaRole() { Name = Roles.Finance });
                await roleManager.CreateAsync(new LuqmaRole() { Name = Roles.Delivery });
                await roleManager.CreateAsync(new LuqmaRole() { Name = Roles.Chef });
                await roleManager.CreateAsync(new LuqmaRole() { Name = Roles.Cashier });

            }
        }
    }
}
