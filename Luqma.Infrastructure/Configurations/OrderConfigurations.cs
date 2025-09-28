using Luqma.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Luqma.Infrastructure.Configurations
{
    public class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(order => order.Id);

            builder.HasMany(order => order.Deliveries)
                .WithOne(delivery => delivery.Order)
                .HasForeignKey(delivery => delivery.OrderId);

            builder.HasOne(order => order.Cashier)
                .WithMany(user => user.Orders)
                .HasForeignKey(order => order.CashierId);

            builder.HasOne(order => order.Customer)
                .WithMany(customer => customer.Orders)
                .HasForeignKey(order => order.CustomerId);

            builder.HasMany(order => order.PaymentsOrders)
                .WithOne(po => po.Order)
                .HasForeignKey(po => po.OrderId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(order => order.OrderTrackings)
                .WithOne(ot => ot.Order)
                .HasForeignKey(ot => ot.OrderId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
