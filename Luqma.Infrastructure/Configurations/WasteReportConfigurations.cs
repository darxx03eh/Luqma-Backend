using Luqma.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Luqma.Infrastructure.Configurations
{
    public class WasteReportConfigurations : IEntityTypeConfiguration<WasteReport>
    {
        public void Configure(EntityTypeBuilder<WasteReport> builder)
        {
            builder.HasKey(wastereport => wastereport.Id);

            builder.HasOne(wastereport => wastereport.MenuItem)
                .WithMany(menuitem => menuitem.WasteReports)
                .HasForeignKey(wastereport => wastereport.ItemId);
        }
    }
}
