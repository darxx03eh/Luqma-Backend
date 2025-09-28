using Luqma.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Luqma.Infrastructure.Configurations
{
    public class BillConfigurations : IEntityTypeConfiguration<Bill>
    {
        public void Configure(EntityTypeBuilder<Bill> builder)
        {
            builder.HasKey(bill => bill.Id);

            builder.HasOne(bill => bill.Finance)
                .WithMany(user => user.Bills)
                .HasForeignKey(bill => bill.FinanceId);
        }
    }
}
