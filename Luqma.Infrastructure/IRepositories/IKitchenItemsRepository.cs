using Luqma.Data.Entities;
using Luqma.Data.Response.KitchenItems;
using Luqma.Data.Wrappers;

namespace Luqma.Infrastructure.IRepositories
{
    public interface IKitchenItemsRepository : IGenericRepository<KitchenItems>
    {
        //public Task<(string, PaginatedResult<GetKitchenItemsResponse>?)> GetPaginatedKitchenItemsAsync(int pageNumber);
    }
}
