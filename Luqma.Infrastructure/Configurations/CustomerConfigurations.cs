using Luqma.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Luqma.Infrastructure.Configurations
{
    public class CustomerConfigurations : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers", customer =>
            {
                customer.HasCheckConstraint("CK_Customer_FirstName_NotEmpty", "LEN([FirstName]) > 0");
                customer.HasCheckConstraint("CK_Customer_LastName_NotEmpty", "LEN([LastName]) > 0");
                customer.HasCheckConstraint(
                    "CK_Customer_PhoneNumber_Format", "[PhoneNumber] LIKE '+970%' OR [PhoneNumber] LIKE '+972%'"
                );
            });
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

            builder.HasMany(customer => customer.Feedbacks)
                .WithOne(feedback => feedback.Customer)
                .HasForeignKey(feedback => feedback.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(customer => customer.FirstName)
                .IsRequired().HasMaxLength(50);
            builder.Property(customer => customer.LastName)
                .IsRequired().HasMaxLength(50);
            builder.Property(customer => customer.PhoneNumber)
                .IsRequired().HasMaxLength(13);
            builder.HasIndex(customer => customer.PhoneNumber)
                .IsUnique();
        }
    }
}
