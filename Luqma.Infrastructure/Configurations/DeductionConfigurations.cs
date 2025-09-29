using Luqma.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Luqma.Infrastructure.Configurations
{
    public class DeductionConfigurations : IEntityTypeConfiguration<Deduction>
    {
        public void Configure(EntityTypeBuilder<Deduction> builder)
        {
            builder.ToTable("Deductions", deduction =>
            {
                deduction.HasCheckConstraint("CK_Deduction_Rate_NonNegative", "[DeductionRate] >= 0");
            });
            builder.HasKey(deduction => new
            {
                deduction.UserId,
                deduction.FinanceId,
                deduction.DeductionDate
            });

            builder.HasOne(deduction => deduction.User)
                .WithMany(user => user.UserDeductions)
                .HasForeignKey(deduction => deduction.UserId);

            builder.HasOne(deduction => deduction.Finance)
                .WithMany(user => user.FinanceDeductions)
                .HasForeignKey(deduction => deduction.FinanceId);

            builder.Property(deduction => deduction.DeductionRate)
                .IsRequired().HasPrecision(5, 2);
        }
    }
}
