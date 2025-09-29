using Luqma.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Luqma.Infrastructure.Configurations
{
    public class MenuItemConfigurations : IEntityTypeConfiguration<MenuItem>
    {
        public void Configure(EntityTypeBuilder<MenuItem> builder)
        {
            builder.HasKey(menuitem => menuitem.Id);

            builder.HasMany(menuitem => menuitem.Feedbacks)
                .WithOne(feedback => feedback.MenuItem)
                .HasForeignKey(feedback => feedback.ItemId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(menuitem => menuitem.MenuContains)
                .WithOne(mc => mc.MenuItem)
                .HasForeignKey(mc => mc.ItemId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(menuitem => menuitem.CategoryItems)
                .WithOne(ci => ci.MenuItem)
                .HasForeignKey(ci => ci.ItemId)
                .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
