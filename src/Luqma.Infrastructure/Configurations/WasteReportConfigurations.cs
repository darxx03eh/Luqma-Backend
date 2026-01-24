using Luqma.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Luqma.Infrastructure.Configurations
{
    public class WasteReportConfigurations : IEntityTypeConfiguration<WasteReport>
    {
        public void Configure(EntityTypeBuilder<WasteReport> builder)
        {
            builder.ToTable("WasteReports", wastereport =>
            {
                wastereport.HasCheckConstraint("CK_WasteReport_WasteQuantity_NonNegative", "[WasteQuantity] >= 0");
            });
            builder.HasKey(wastereport => wastereport.Id);

            builder.HasOne(wastereport => wastereport.MenuItem)
                .WithMany(menuitem => menuitem.WasteReports)
                .HasForeignKey(wastereport => wastereport.ItemId);

            builder.Property(wastereport => wastereport.Reason)
                   .HasMaxLength(500);

            builder.Property(wastereport => wastereport.WasteQuantity)
                   .IsRequired().HasPrecision(10, 2);
        }
    }
}
