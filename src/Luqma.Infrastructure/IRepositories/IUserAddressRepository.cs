using Luqma.Data.Entities;
using Luqma.Data.Response.Users;
using Luqma.Data.Wrappers;

namespace Luqma.Infrastructure.IRepositories
{
    public interface IUserAddressRepository : IGenericRepository<UserAddress>
    {
        public Task<(string, PaginatedResult<ShowUserAddressResponse>?)> GetUserAddressesAsync(int id, int pageNumber, int pageSize);
    }
}
