using Luqma.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Luqma.Infrastructure.Configurations
{
    public class MenuConfigurations : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.ToTable("Menus");
            builder.HasKey(menu => menu.Id);

            builder.HasMany(menu => menu.MenuContains)
                .WithOne(mc => mc.Menu)
                .HasForeignKey(mc => mc.MenuId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(m => m.Title)
                   .IsRequired()
                   .HasMaxLength(100);
            builder.Property(m => m.Description)
                   .HasMaxLength(500);
        }
    }
}
