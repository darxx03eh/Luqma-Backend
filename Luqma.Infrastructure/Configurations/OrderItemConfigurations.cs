using Luqma.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Luqma.Infrastructure.Configurations
{
    public class OrderItemConfigurations : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItems", oi =>
            {
                oi.HasCheckConstraint("CK_OrderItem_Quantity_Positive", "[Quantity] > 0");
            });
            builder.HasKey(oi => oi.Id);

            builder.HasOne(oi => oi.Order)
                .WithMany(order => order.OrderItems)
                .HasForeignKey(oi => oi.OrderId);

            builder.HasOne(oi => oi.MenuItem)
                .WithMany(menuitem => menuitem.OrderItems)
                .HasForeignKey(oi => oi.ItemId);
             
            

            builder.Property(oi => oi.Quantity)
                   .IsRequired().HasPrecision(10, 2);

        }
    }
}
