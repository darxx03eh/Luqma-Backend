using Luqma.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Luqma.Infrastructure.Configurations
{
    public class RequirmentItemsConfigurations : IEntityTypeConfiguration<RequirmentItems>
    {
        public void Configure(EntityTypeBuilder<RequirmentItems> builder)
        {
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

        }
    }
}
