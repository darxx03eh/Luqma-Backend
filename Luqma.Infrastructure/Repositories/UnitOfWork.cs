using Luqma.Data.Entities.Identity;
using Luqma.Infrastructure.Data;
using Luqma.Infrastructure.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Luqma.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LuqmaDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<LuqmaUser> userManager;

        public UnitOfWork(LuqmaDbContext context, IHttpContextAccessor httpContextAccessor, UserManager<LuqmaUser> userManager)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            RefreshTokenRepository = new RefreshTokenRepository(context, httpContextAccessor);
            UserRepository = new UserRepository(context, httpContextAccessor, userManager);
        }

        public IUserRepository UserRepository { get; set; }
        public IRefreshTokenRepository RefreshTokenRepository { get; set; }
    }
}
