using Luqma.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Luqma.Infrastructure.Configurations
{
    public class PaymentConfigurations : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(payment => payment.Id);

            builder.HasMany(payment => payment.PaymentsOrders)
                .WithOne(po => po.Payment)
                .HasForeignKey(po => po.PaymentId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
