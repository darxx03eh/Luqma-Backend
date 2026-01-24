using Luqma.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Luqma.Infrastructure.Configurations
{
    public class PredictionConfigurations : IEntityTypeConfiguration<Prediction>
    {
        public void Configure(EntityTypeBuilder<Prediction> builder)
        {
            builder.ToTable("Predictions", prediction =>
            {
                prediction.HasCheckConstraint("CK_Prediction_PredictedQuantity_NonNegative", "[PredictedQuantity] >= 0");
                prediction.HasCheckConstraint("CK_Prediction_ConfidenceScore_Valid", "[ConfidenceScore] >= 0 AND [ConfidenceScore] <= 1");
            });
            builder.HasKey(prediction => prediction.Id);

            builder.HasOne(prediction => prediction.MenuItem)
                .WithMany(menuitem => menuitem.Predictions)
                .HasForeignKey(menuitem => menuitem.ItemId);

            builder.Property(p => p.PredictedQuantity)
                   .IsRequired().HasPrecision(10, 2);
            builder.Property(p => p.ConfidenceScore)
                   .IsRequired().HasPrecision(5, 4);

        }
    }
}
