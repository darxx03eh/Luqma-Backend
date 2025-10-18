using Luqma.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Luqma.Infrastructure.Configurations
{
    public class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders", order =>
            {
                order.HasCheckConstraint("CK_Order_TotalPrice_NonNegative", "[TotalPrice] >= 0");
            });
            builder.HasKey(order => order.Id);

            builder.HasMany(order => order.Deliveries)
                .WithOne(delivery => delivery.Order)
                .HasForeignKey(delivery => delivery.OrderId);

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

            builder.HasMany(order => order.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(o => o.Status)
                   .IsRequired().HasMaxLength(50)
                   .HasDefaultValue("Pending");
            builder.Property(o => o.Type)
                   .IsRequired().HasMaxLength(50);
            builder.Property(o => o.DayOfWeek)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(o => o.CustomerType)
                   .IsRequired().HasMaxLength(50);
            builder.Property(o => o.Note)
                   .HasMaxLength(500);
            builder.Property(o => o.WeatherConditions)
                   .HasMaxLength(100);
            builder.Property(o => o.EventTag)
                   .HasMaxLength(100);
            builder.Property(o => o.TotalPrice)
                   .IsRequired().HasPrecision(10, 2);
            builder.Property(o => o.IsHoliday)
                   .IsRequired();
            builder.Property(o => o.IsWeekend)
                   .IsRequired();
            builder.Property(o => o.PromitionApplied)
                   .IsRequired();
        }
    }
}
