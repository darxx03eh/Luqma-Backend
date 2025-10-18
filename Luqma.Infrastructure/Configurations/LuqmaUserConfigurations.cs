using Luqma.Data.Entities;
using Luqma.Data.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Luqma.Infrastructure.Configurations
{
    public class LuqmaUserConfigurations : IEntityTypeConfiguration<LuqmaUser>
    {
        public void Configure(EntityTypeBuilder<LuqmaUser> builder)
        {
            builder.HasOne(user => user.Manager)
                .WithMany(user => user.Subordinates)
                .HasForeignKey(user => user.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(user => user.UserRefreshTokens)
                .WithOne(refresh => refresh.User)
                .HasForeignKey(refresh => refresh.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(user => user.UserSalaries)
                .WithOne(salary => salary.User)
                .HasForeignKey(salary => salary.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(user => user.FinanceSalaries)
                .WithOne(salary => salary.Finance)
                .HasForeignKey(salary => salary.FinanceId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(user => user.Addresses)
                .WithOne(address => address.User)
                .HasForeignKey(address => address.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(user => user.UserDeductions)
                .WithOne(deduction => deduction.User)
                .HasForeignKey(deduction => deduction.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(user => user.FinanceDeductions)
                .WithOne(deduction => deduction.Finance)
                .HasForeignKey(deduction => deduction.FinanceId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(user => user.Bills)
                .WithOne(bill => bill.Finance)
                .HasForeignKey(bill => bill.FinanceId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(user => user.KitchenRequirments)
                .WithOne(kr => kr.Chef)
                .HasForeignKey(kr => kr.ChefId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(user => user.Deliveries)
                .WithOne(delivery => delivery.Delivery)
                .HasForeignKey(delivery => delivery.DeliveryId)
                .OnDelete(DeleteBehavior.Cascade);

           

            builder.Property(u => u.FirstName)
                   .IsRequired().HasMaxLength(50);
            builder.Property(u => u.LastName)
                   .IsRequired().HasMaxLength(50);
            builder.Property(u => u.BirthDate)
                   .IsRequired();
            builder.Property(u => u.Salary)
                   .HasPrecision(18, 2);
            builder.Property(u => u.IsActive)
                   .HasDefaultValue(true);
        }
    }
}
