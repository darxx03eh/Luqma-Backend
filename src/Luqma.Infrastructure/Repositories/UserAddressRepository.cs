using Luqma.Data.Entities;
using Luqma.Data.Response.Users;
using Luqma.Data.Wrappers;
using Luqma.Infrastructure.Data;
using Luqma.Infrastructure.IRepositories;
using Microsoft.AspNetCore.Http;

namespace Luqma.Infrastructure.Repositories
{
    public class UserAddressRepository : GenericRepository<UserAddress>, IUserAddressRepository
    {
        private readonly LuqmaDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserAddressRepository(LuqmaDbContext context, IHttpContextAccessor httpContextAccessor)
            : base(context, httpContextAccessor)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<(string, PaginatedResult<ShowUserAddressResponse>?)> GetUserAddressesAsync(int id, int pageNumber, int pageSize)
        {
            var addressesQueryable = GetTableNoTracking().OrderBy(address => address.State)
                                                .OrderBy(address => address.City)
                                                .OrderBy(address => address.Street).Where(address => address.UserId.Equals(id))
                                                .AsQueryable();
            if (addressesQueryable is null)
                return ("AddressesNotFound", null);

            var addresses = await addressesQueryable.Select(address => new ShowUserAddressResponse()
            {
                Id = address.Id,
                City = address.City,
                State = address.State,
                Street = address.Street
            }).ToPaginatedListAsync(pageNumber, pageSize);
            return addresses.Data.Count() > 0 ? ("AddressesFound", addresses) : ("AddressesNotFound", null);
        }
    }
}
