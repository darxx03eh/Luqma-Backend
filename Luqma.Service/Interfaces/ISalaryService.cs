using Luqma.Data.Response.Salaries;
using Luqma.Data.Wrappers;

namespace Luqma.Service.Interfaces
{
    public interface ISalaryService
    {
        public Task<string> GenerateSalaryAsync();
        public Task<string> GenerateSalaryForUserAsync(int userId, int? year = 0, int? month = 0);
        public Task<string> DeleteSalaryAsync(int id);
        public Task<string> ChangeSalaryStatusAsync(int id, string status);
        public Task<string> ChangeSalaryAmountAsync(int id, double amount);
        public Task<(string, PaginatedResult<GetSalariesResponse>?)> GetSalariesAsync(int pageNumber, string search, string filter, 
                                                                                      int? year = 0, int? month = 0);
    }
}
