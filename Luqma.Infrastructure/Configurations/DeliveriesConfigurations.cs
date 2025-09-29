using Luqma.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Luqma.Infrastructure.Configurations
{
    public class DeliveriesConfigurations : IEntityTypeConfiguration<Deliveries>
    {
        public void Configure(EntityTypeBuilder<Deliveries> builder)
        {
            builder.ToTable("Deliveries");
            builder.HasKey(delivery => new
            {
                delivery.DeliveryId,
                delivery.OrderId,
            });

            builder.HasOne(delivery => delivery.Delivery)
                .WithMany(user => user.Deliveries)
                .HasForeignKey(delivery => delivery.DeliveryId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(delivery => delivery.Order)
                .WithMany(order => order.Deliveries)
                .HasForeignKey(delivery => delivery.OrderId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
