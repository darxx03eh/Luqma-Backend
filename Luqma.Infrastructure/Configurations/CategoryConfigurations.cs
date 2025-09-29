using Luqma.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Luqma.Infrastructure.Configurations
{
    public class CategoryConfigurations : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories", category =>
            {
                category.HasCheckConstraint("CK_Category_Title_NotEmpty", "LEN([Title]) > 0");
            });
            builder.HasKey(category => category.Id);

            builder.HasMany(category => category.CategoryItems)
                .WithOne(ci => ci.Category)
                .HasForeignKey(ci => ci.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(category => category.Title)
                .IsRequired().HasMaxLength(100);
            builder.HasIndex(category => category.Title)
                .IsUnique();

        }
    }
}
