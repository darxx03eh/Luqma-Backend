using Luqma.Data.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Luqma.Infrastructure.Configurations
{
    public class LuqmaUserConfigurations : IEntityTypeConfiguration<LuqmaUser>
    {
        public void Configure(EntityTypeBuilder<LuqmaUser> builder)
        {
            builder.HasOne(user => user.Manager)
                .WithMany(user => user.Subordinates)
                .HasForeignKey(user => user.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(user => user.UserRefreshTokens)
                .WithOne(refresh => refresh.User)
                .HasForeignKey(refresh => refresh.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
