using Luqma.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Luqma.Infrastructure.Configurations
{
    public class BillConfigurations : IEntityTypeConfiguration<Bill>
    {
        public void Configure(EntityTypeBuilder<Bill> builder)
        {
            builder.ToTable("Bills", bill =>
            {
                bill.HasCheckConstraint("CK_Bill_TotalPrice_NonNegative", "[TotalPrice] >= 0");
            });

            builder.HasKey(bill => bill.Id);

            builder.HasOne(bill => bill.Finance)
                .WithMany(user => user.Bills)
                .HasForeignKey(bill => bill.FinanceId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(bill => bill.BillType)
                .IsRequired().HasMaxLength(50);
            builder.Property(bill => bill.Note)
                .HasMaxLength(500);
            builder.Property(bill => bill.TotalPrice)
                .HasDefaultValue(0.0);

        }
    }
}
