using Luqma.Data.Entities;
using Luqma.Data.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Luqma.Infrastructure.Data
{
    public class LuqmaDbContext
        : IdentityDbContext<LuqmaUser, LuqmaRole, int,
          IdentityUserClaim<int>, IdentityUserRole<int>, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DbSet<LuqmaUser> Users { get;set; }
        public DbSet<LuqmaRole> Roles { get;set; }
        public DbSet<UserRefreshToken> UserRefreshTokens { get;set; }
        public DbSet<Deduction> Deductions { get;set; }
        public DbSet<Salary> Salaries { get;set; }
        public DbSet<UserAddress> UserAddresses { get;set; }
        public LuqmaDbContext(DbContextOptions<LuqmaDbContext> options)
            : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
