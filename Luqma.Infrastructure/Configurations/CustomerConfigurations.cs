using Luqma.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Luqma.Infrastructure.Configurations
{
    public class CustomerConfigurations : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(customer => customer.Id);

            builder.HasMany(customer => customer.Orders)
                .WithOne(order => order.Customer)
                .HasForeignKey(order => order.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(customer => customer.Addresses)
                .WithOne(address => address.Customer)
                .HasForeignKey(address => address.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(customer => customer.OrderTrackings)
                .WithOne(ot => ot.Customer)
                .HasForeignKey(ot => ot.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
