using Luqma.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Luqma.Infrastructure.Configurations
{
    public class CategoryItemConfigurations : IEntityTypeConfiguration<CategoryItem>
    {
        public void Configure(EntityTypeBuilder<CategoryItem> builder)
        {
            builder.HasKey(ci => new
            {
                ci.CategoryId,
                ci.ItemId,
            });

            builder.HasOne(ci => ci.Category)
                .WithMany(category => category.CategoryItems)
                .HasForeignKey(ci => ci.CategoryId);

            builder.HasOne(ci => ci.MenuItem)
                .WithMany(menuitem => menuitem.CategoryItems)
                .HasForeignKey(ci => ci.ItemId);
        }
    }
}
