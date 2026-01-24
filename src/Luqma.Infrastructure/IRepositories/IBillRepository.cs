using Luqma.Data.Entities;
using Luqma.Data.Response.Bills;
using Luqma.Data.Wrappers;

namespace Luqma.Infrastructure.IRepositories
{
    public interface IBillRepository : IGenericRepository<Bill>
    {
        public Task<(string, PaginatedResult<BillsResponse>?)> GetBillsAsync(int pageNumber);
    }
}
