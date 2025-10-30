using Luqma.Data.Entities;

namespace Luqma.Infrastructure.IRepositories
{
    public interface IKitchenItemsRepository : IGenericRepository<KitchenItems>
    {
        //public Task<(string, PaginatedResult<GetKitchenItemsResponse>?)> GetPaginatedKitchenItemsAsync(int pageNumber);
    }
}
