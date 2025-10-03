using Luqma.Data.Response.Deductions;
using Luqma.Data.Wrappers;

namespace Luqma.Service.Interfaces
{
    public interface IDeductionService
    {
        public Task<string> AddDeductionToUserAsync(int userId, double deductionRate);
        public Task<string> RemoveDeductionFromUserAsync(int id);
        public Task<string> UpdateDeductionAsync(int deductionId, double deductionRate);
        public Task<(string, PaginatedResult<ViewDeductionsResponse>?)> ShowAllDeductionsAsync(int pageNumber);
        public Task<(string, PaginatedResult<ViewDeductionsResponse>?)> GetAllDeductionsByDateAsync(int pageNumber, int year, int month);
        public Task<(string, PaginatedResult<ViewDeductionsResponse>?)> GetAllDeductionsForSpecificUser(int pageNumber, string name);
    }
}
