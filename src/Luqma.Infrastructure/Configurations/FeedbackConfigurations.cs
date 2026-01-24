using Luqma.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Luqma.Infrastructure.Configurations
{
    public class FeedbackConfigurations : IEntityTypeConfiguration<Feedback>
    {
        public void Configure(EntityTypeBuilder<Feedback> builder)
        {
            builder.ToTable("Feedbacks", feedback =>
            {
                feedback.HasCheckConstraint("CK_Feedback_Stars_Range", "[Stars] >= 1 AND [Stars] <= 5");
            });
            builder.HasKey(feedback => feedback.Id);

            builder.HasOne(feedback => feedback.Customer)
                .WithMany(customer => customer.Feedbacks)
                .HasForeignKey(feedback => feedback.CustomerId);

            builder.HasOne(feedback => feedback.MenuItem)
                .WithMany(menuitem => menuitem.Feedbacks)
                .HasForeignKey(feedback => feedback.ItemId);

            builder.Property(feedback => feedback.Content)
                .HasMaxLength(500);
            builder.Property(feedback => feedback.Stars)
                .IsRequired();
        }
    }
}
