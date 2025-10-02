using Luqma.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Luqma.Infrastructure.Configurations
{
    public class CustomerAddressConfigurations : IEntityTypeConfiguration<CustomerAddress>
    {
        public void Configure(EntityTypeBuilder<CustomerAddress> builder)
        {
            builder.ToTable("CustomerAddresses", address =>
            {
                address.HasCheckConstraint("CK_CustomerAddress_City_NotEmpty", "LEN([City]) > 0");
                address.HasCheckConstraint("CK_CustomerAddress_State_NotEmpty", "LEN([State]) > 0");
                address.HasCheckConstraint("CK_CustomerAddress_Street_NotEmpty", "LEN([Street]) > 0");

            });
            builder.HasKey(address => address.Id);

            builder.HasOne(address => address.Customer)
                .WithMany(customer => customer.Addresses)
                .HasForeignKey(address => address.CustomerId);

            builder.Property(address => address.City)
                .IsRequired().HasMaxLength(50);
            builder.Property(address => address.State)
                .IsRequired().HasMaxLength(50);
            builder.Property(address => address.Street)
                .IsRequired().HasMaxLength(50);
        }
    }
}
