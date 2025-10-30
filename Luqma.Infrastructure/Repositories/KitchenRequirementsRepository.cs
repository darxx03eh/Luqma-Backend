using Luqma.Data.Entities;
using Luqma.Infrastructure.Data;
using Luqma.Infrastructure.IRepositories;
using Microsoft.AspNetCore.Http;

namespace Luqma.Infrastructure.Repositories
{
    public class KitchenRequirementsRepository : GenericRepository<KitchenRequirements>, IKitchenRequirementsRepository
    {
        private readonly LuqmaDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;

        public KitchenRequirementsRepository(LuqmaDbContext context, IHttpContextAccessor httpContextAccessor) 
            : base(context, httpContextAccessor)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
        }
    }
}
