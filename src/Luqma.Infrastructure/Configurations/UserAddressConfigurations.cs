using Luqma.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Luqma.Infrastructure.Configurations
{
    public class UserAddressConfigurations : IEntityTypeConfiguration<UserAddress>
    {
        public void Configure(EntityTypeBuilder<UserAddress> builder)
        {
            builder.ToTable("UserAddresses", address =>
            {
                address.HasCheckConstraint("CK_UserAddress_City_NotEmpty", "LEN([City]) > 0");
                address.HasCheckConstraint("CK_UserAddress_State_NotEmpty", "LEN([State]) > 0");
                address.HasCheckConstraint("CK_UserAddress_Street_NotEmpty", "LEN([Street]) > 0");
            });
            builder.HasKey(address => address.Id);

            builder.HasOne(address => address.User)
                .WithMany(user => user.Addresses)
                .HasForeignKey(address => address.UserId);

            builder.Property(address => address.City)
                   .HasMaxLength(50);
            builder.Property(address => address.State)
                   .HasMaxLength(50);
            builder.Property(address => address.Street)
                   .HasMaxLength(100);
        }
    }
}
