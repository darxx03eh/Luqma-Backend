using Luqma.Data.Entities;
using Luqma.Data.Response.Feedbacks;
using Luqma.Data.Wrappers;

namespace Luqma.Infrastructure.IRepositories
{
    public interface IMenuItemRepository : IGenericRepository<MenuItem>
    {
        public Task<bool> IsIdExistAsync(int id);
        public Task<(string, PaginatedResult<GetCustomerFeedback>?)> GetItemFeedbacksAsync(int id, int pageNumber, int pageSize);
        public Task<List<MenuItem>> GetAllMenuItemsAsync();
    }
}
