using Luqma.Data.Entities.Identity;
using Luqma.Data.Response.Users;
using Luqma.Data.Wrappers;

namespace Luqma.Infrastructure.IRepositories
{
    public interface IUserRepository : IGenericRepository<LuqmaUser>
    {
        public Task<(string, PaginatedResult<ViewUsersResponse>?)> ViewUsersAsync(int pageNumber, int pageSize);
    }
}
