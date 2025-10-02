using Luqma.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Luqma.Infrastructure.Configurations
{
    public class MenuContainsConfigurations : IEntityTypeConfiguration<MenuContains>
    {
        public void Configure(EntityTypeBuilder<MenuContains> builder)
        {
            builder.ToTable("MenuContains");
            builder.HasKey(mc => mc.Id);

            builder.HasOne(mc => mc.Menu)
                .WithMany(menu => menu.MenuContains)
                .HasForeignKey(mc => mc.MenuId);

            builder.HasOne(mc => mc.MenuItem)
                .WithMany(menuitem => menuitem.MenuContains)
                .HasForeignKey(mc => mc.ItemId);
        }
    }
}
