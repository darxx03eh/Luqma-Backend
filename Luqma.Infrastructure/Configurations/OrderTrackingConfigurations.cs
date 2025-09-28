using Luqma.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Luqma.Infrastructure.Configurations
{
    public class OrderTrackingConfigurations : IEntityTypeConfiguration<OrderTracking>
    {
        public void Configure(EntityTypeBuilder<OrderTracking> builder)
        {
            builder.HasKey(ot => new
            {
                ot.OrderId,
                ot.CustomerId,
            });

            builder.HasOne(ot => ot.Customer)
                .WithMany(customer => customer.OrderTrackings)
                .HasForeignKey(ot => ot.CustomerId);

            builder.HasOne(ot => ot.Order)
                .WithMany(order => order.OrderTrackings)
                .HasForeignKey(ot => ot.OrderId);
        }
    }
}
