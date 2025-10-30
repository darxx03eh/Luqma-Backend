using Luqma.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Luqma.Infrastructure.Configurations
{
    public class KitchenRequirmentsConfigurations : IEntityTypeConfiguration<KitchenRequirements>
    {
        public void Configure(EntityTypeBuilder<KitchenRequirements> builder)
        {
            builder.ToTable("KitchenRequirements", kr =>
            {
                kr.HasCheckConstraint("CK_KitchenRequirements_TotalPrice_NonNegative", "[TotalPrice] >= 0");
            });
            builder.HasKey(kr => kr.Id);

            builder.HasOne(kr => kr.Chef)
                .WithMany(user => user.KitchenRequirements)
                .HasForeignKey(kr => kr.ChefId);

            builder.HasMany(kr => kr.RequirementItems)
                .WithOne(ri => ri.KitchenRequirements)
                .HasForeignKey(ri => ri.RequirmentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(kr => kr.Status)
                   .IsRequired()
                   .HasMaxLength(50);
            builder.Property(kr => kr.Note)
                   .HasMaxLength(500);
            builder.Property(kr => kr.TotalPrice)
                   .IsRequired()
                   .HasPrecision(10, 2);
        }
    }
}
