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
        #region Tables
        public DbSet<LuqmaUser> Users { get; set; }
        public DbSet<LuqmaRole> Roles { get; set; }
        public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
        public DbSet<Deduction> Deductions { get; set; }
        public DbSet<Salary> Salaries { get; set; }
        public DbSet<UserAddress> UserAddresses { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<KitchenRequirments> KitchenRequirments { get; set; }
        public DbSet<KitchenItems> KitchenItems { get; set; }
        public DbSet<RequirmentItems> RequirmentItems { get; set; }
        public DbSet<Deliveries> Deliveries { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerAddress> CustomerAddresses { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentsOrder> PaymentsOrders { get; set; }
        public DbSet<OrderTracking> OrderTrackings { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuContains> MenuContains { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryItem> CategoryItems { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        #endregion
        public LuqmaDbContext(DbContextOptions<LuqmaDbContext> options)
            : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
