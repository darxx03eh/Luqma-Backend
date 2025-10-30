using Luqma.Data.Entities;
using Luqma.Infrastructure.Data;
using Luqma.Infrastructure.IRepositories;
using Microsoft.AspNetCore.Http;

namespace Luqma.Infrastructure.Repositories
{
    public class RequirementItemsRepository : GenericRepository<RequirementItems>, IRequirementItemsRepository
    {
        private readonly LuqmaDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;

        public RequirementItemsRepository(LuqmaDbContext context, IHttpContextAccessor httpContextAccessor) 
            : base(context, httpContextAccessor)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
        }
    }
}
