using Luqma.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Luqma.Infrastructure.Configurations
{
    public class MenuItemConfigurations : IEntityTypeConfiguration<MenuItem>
    {
        public void Configure(EntityTypeBuilder<MenuItem> builder)
        {
            builder.ToTable("MenuItems", menuitem =>
            {
                menuitem.HasCheckConstraint("CK_MenuItem_Price_NonNegative", "[Price] >= 0");
                menuitem.HasCheckConstraint("CK_MenuItem_Discount_Valid", "[Discount] IS NULL OR [Discount] >= 0 AND [Discount] <= 100");
                menuitem.HasCheckConstraint("CK_MenuItem_TotalStars_Range", "[TotalStars] >= 0 AND [TotalStars] <= 5");
            });
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

            builder.HasMany(menuitem => menuitem.OrderItems)
                .WithOne(oi => oi.MenuItem)
                .HasForeignKey(oi => oi.ItemId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(menuitem => menuitem.Predictions)
                .WithOne(prediction => prediction.MenuItem)
                .HasForeignKey(prediction => prediction.ItemId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(menuitem => menuitem.WasteReports)
                .WithOne(wastereport => wastereport.MenuItem)
                .HasForeignKey(wastereport => wastereport.ItemId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(menuitem => menuitem.Carts)
                    .WithOne(cart => cart.MenuItem)
                    .HasForeignKey(cart => cart.ItemId)
                    .OnDelete(DeleteBehavior.Cascade);

            builder.Property(mi => mi.Item)
                   .IsRequired()
                   .HasMaxLength(100);
            builder.Property(mi => mi.Description)
                   .HasMaxLength(500);
            builder.Property(mi => mi.Price)
                   .IsRequired()
                   .HasPrecision(10, 2);
            builder.Property(mi => mi.Discount)
                   .HasPrecision(5, 2);
            builder.Property(mi => mi.IsVegetarian)
                   .IsRequired();
            builder.Property(mi => mi.TotalStars)
                .HasDefaultValue(0);
            builder.Property(mi => mi.Status)
                .IsRequired();
        }
    }
}
