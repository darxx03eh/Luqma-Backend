using Luqma.Data.Entities;
using Luqma.Data.Response.Salaries;
using Luqma.Data.Wrappers;

namespace Luqma.Infrastructure.IRepositories
{
    public interface ISalaryRepository : IGenericRepository<Salary>
    {
        public Task<(string, PaginatedResult<GetSalariesResponse>?)> GetSalariesAsync(int pageNumber, string search, string filter, 
                                                                                      int? year = 0, int? month = 0);

    }
}
