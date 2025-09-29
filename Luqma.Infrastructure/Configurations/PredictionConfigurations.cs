using Luqma.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Luqma.Infrastructure.Configurations
{
    public class PredictionConfigurations : IEntityTypeConfiguration<Prediction>
    {
        public void Configure(EntityTypeBuilder<Prediction> builder)
        {
            builder.HasKey(prediction => prediction.Id);

            builder.HasOne(prediction => prediction.MenuItem)
                .WithMany(menuitem => menuitem.Predictions)
                .HasForeignKey(menuitem => menuitem.ItemId);
        }
    }
}
