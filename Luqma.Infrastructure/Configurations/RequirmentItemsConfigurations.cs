using Luqma.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Luqma.Infrastructure.Configurations
{
    public class RequirmentItemsConfigurations : IEntityTypeConfiguration<RequirmentItems>
    {
        public void Configure(EntityTypeBuilder<RequirmentItems> builder)
        {
            builder.ToTable("RequirmentItems", ri =>
            {
                ri.HasCheckConstraint("CK_RequirmentItems_Price_NonNegative", "[Price] >= 0");
                ri.HasCheckConstraint("CK_RequirmentItems_Discount_Valid", "[Discount] >= 0 AND [Discount] <= 100");
            });
            builder.HasKey(ri => new
            {
                ri.ItemId,
                ri.RequirmentId,
            });

            builder.HasOne(ri => ri.KitchenItems)
                .WithMany(ki => ki.RequirmentItems)
                .HasForeignKey(ri => ri.ItemId);

            builder.HasOne(ri => ri.KitchenRequirments)
                .WithMany(kr => kr.RequirmentItems)
                .HasForeignKey(ri => ri.RequirmentId);

            builder.Property(ri => ri.Price)
                   .IsRequired().HasPrecision(10, 2);
            builder.Property(ri => ri.Discount)
                   .IsRequired().HasPrecision(5, 2);
        }
    }
}
