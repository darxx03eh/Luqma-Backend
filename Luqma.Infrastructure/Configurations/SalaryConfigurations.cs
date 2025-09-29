using Luqma.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Luqma.Infrastructure.Configurations
{
    public class SalaryConfigurations : IEntityTypeConfiguration<Salary>
    {
        public void Configure(EntityTypeBuilder<Salary> builder)
        {
            builder.ToTable("Salaries");
            builder.HasKey(salary => new
            {
                salary.UserId,
                salary.FinanceId,
                salary.SalaryDate
            });

            builder.HasOne(salary => salary.User)
                .WithMany(user => user.UserSalaries)
                .HasForeignKey(salary => salary.UserId);

            builder.HasOne(salary => salary.Finance)
                .WithMany(user => user.FinanceSalaries)
                .HasForeignKey(salary => salary.FinanceId);

            builder.Property(s => s.Status)
                   .IsRequired().HasMaxLength(50);
        }
    }
}
