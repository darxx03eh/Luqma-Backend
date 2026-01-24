using Luqma.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Luqma.Infrastructure.Configurations
{
    public class KitchenItemsConfigurations : IEntityTypeConfiguration<KitchenItems>
    {
        public void Configure(EntityTypeBuilder<KitchenItems> builder)
        {
            builder.ToTable("KitchenItems", ki =>
            {
                ki.HasCheckConstraint("CK_KitchenItems_Quantity_NonNegative", "[Quantity] >= 0");
            });
            builder.HasKey(ki => ki.Id);

            builder.HasMany(ki => ki.RequirementItems)
                .WithOne(ri => ri.KitchenItems)
                .HasForeignKey(ri => ri.ItemId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(ki => ki.Item)
                   .IsRequired()
                   .HasMaxLength(100);
            builder.Property(ki => ki.Unit)
                   .IsRequired()
                   .HasMaxLength(20);
            builder.Property(ki => ki.Note)
                   .HasMaxLength(500);
            builder.Property(ki => ki.Quantity)
                   .IsRequired()
                   .HasPrecision(10, 2);
        }
    }
}
