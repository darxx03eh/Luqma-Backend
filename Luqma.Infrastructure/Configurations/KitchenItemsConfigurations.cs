using Luqma.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Luqma.Infrastructure.Configurations
{
    public class KitchenItemsConfigurations : IEntityTypeConfiguration<KitchenItems>
    {
        public void Configure(EntityTypeBuilder<KitchenItems> builder)
        {
            builder.HasKey(ki => ki.Id);

            builder.HasMany(ki => ki.RequirmentItems)
                .WithOne(ri => ri.KitchenItems)
                .HasForeignKey(ri => ri.ItemId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
