using Luqma.Data.Entities.Identity;
using Luqma.Infrastructure.Data;
using Luqma.Infrastructure.IRepositories;
using Microsoft.AspNetCore.Http;

namespace Luqma.Infrastructure.Repositories
{
    public class RefreshTokenRepository : GenericRepository<UserRefreshToken>, IRefreshTokenRepository
    {
        private readonly LuqmaDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;

        public RefreshTokenRepository(LuqmaDbContext context, IHttpContextAccessor httpContextAccessor)
            : base(context, httpContextAccessor)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
        }
    }
}
