using Luqma.Data.Entities;
using Luqma.Data.Response.Deductions;
using Luqma.Data.Wrappers;

namespace Luqma.Infrastructure.IRepositories
{
    public interface IDeductionRepository : IGenericRepository<Deduction>
    {
        public Task<(string, PaginatedResult<ViewDeductionsResponse>?)> GetAllDeductionsByDateAsync(int pageNumber, int year, int month);
        public Task<(string, PaginatedResult<ViewDeductionsResponse>?)> GetAllDeductionsForSpecificUser(int pageNumber, string name);
        public Task<(string, Dictionary<int, double>?)> GetTotalDeductionsForEachUser(int year, int month);
    }
}
