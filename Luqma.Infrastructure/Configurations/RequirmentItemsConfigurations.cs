using Luqma.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Luqma.Infrastructure.Configurations
{
    public class RequirmentItemsConfigurations : IEntityTypeConfiguration<RequirementItems>
    {
        public void Configure(EntityTypeBuilder<RequirementItems> builder)
        {
            builder.ToTable("RequirementItems", ri =>
            {
                ri.HasCheckConstraint("CK_RequirementItems_Price_NonNegative", "[Price] >= 0");
                ri.HasCheckConstraint("CK_RequirementItems_Discount_Valid", "[Discount] >= 0 AND [Discount] <= 100");
            });
            builder.HasKey(ri => ri.Id);

            builder.HasOne(ri => ri.KitchenItems)
                .WithMany(ki => ki.RequirementItems)
                .HasForeignKey(ri => ri.ItemId);

            builder.HasOne(ri => ri.KitchenRequirements)
                .WithMany(kr => kr.RequirementItems)
                .HasForeignKey(ri => ri.RequirmentId);

            builder.Property(ri => ri.Price)
                   .IsRequired().HasPrecision(10, 2);
            builder.Property(ri => ri.Discount)
                   .IsRequired().HasPrecision(5, 2);
        }
    }
}
