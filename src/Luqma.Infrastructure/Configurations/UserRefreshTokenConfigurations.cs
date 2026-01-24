using Luqma.Data.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Luqma.Infrastructure.Configurations
{
    public class UserRefreshTokenConfigurations : IEntityTypeConfiguration<UserRefreshToken>
    {
        public void Configure(EntityTypeBuilder<UserRefreshToken> builder)
        {
            builder.ToTable("UserRefreshTokens");
            builder.HasOne(refresh => refresh.User)
                .WithMany(user => user.UserRefreshTokens)
                .HasForeignKey(refresh => refresh.UserId);
        }
    }
}
