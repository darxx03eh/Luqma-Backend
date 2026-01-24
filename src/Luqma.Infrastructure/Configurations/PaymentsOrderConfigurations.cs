using Luqma.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Luqma.Infrastructure.Configurations
{
    public class PaymentsOrderConfigurations : IEntityTypeConfiguration<PaymentsOrder>
    {
        public void Configure(EntityTypeBuilder<PaymentsOrder> builder)
        {
            builder.ToTable("PaymentsOrders");
            builder.HasKey(po => po.Id);

            builder.HasOne(po => po.Order)
                .WithMany(order => order.PaymentsOrders)
                .HasForeignKey(po => po.OrderId);

            builder.HasOne(po => po.Payment)
                .WithMany(payment => payment.PaymentsOrders)
                .HasForeignKey(po => po.PaymentId);
        }
    }
}
