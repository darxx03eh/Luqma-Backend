using Luqma.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Luqma.Infrastructure.Configurations
{
    public class PaymentConfigurations : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Payments", payment =>
            {
                payment.HasCheckConstraint("CK_Payment_Amount_Positive", "[Amount] > 0");
            });
            builder.HasKey(payment => payment.Id);

            builder.HasMany(payment => payment.PaymentsOrders)
                .WithOne(po => po.Payment)
                .HasForeignKey(po => po.PaymentId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(payment => payment.Amount)
                   .IsRequired().HasPrecision(10, 2);
            builder.Property(payment => payment.Status)
                   .IsRequired().HasMaxLength(50);
            builder.Property(payment => payment.PaymentMethod)
                   .IsRequired().HasMaxLength(50);
            builder.Property(payment => payment.Currency)
                   .IsRequired().HasMaxLength(10).HasDefaultValue("NIS");
            builder.Property(payment => payment.Note)
                   .HasMaxLength(500);
        }
    }
}
