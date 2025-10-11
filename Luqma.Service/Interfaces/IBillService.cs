using Luqma.Data.Response.Bills;
using Luqma.Data.Wrappers;

namespace Luqma.Service.Interfaces
{
    public interface IBillService
    {
        public Task<(string, BillsResponse?)> AddNewBillAsync(string billType, double totalPrice, string note);
        public Task<(string, PaginatedResult<BillsResponse>?)> GetBillsAsync(int pageNumber);
        public Task<string> UpdateBillStatusAsync(int id, string status);
        public Task<string> UpdateBillAsync(int id, string billType, string note, double totalPrice);
        public Task<string> DeleteBillAsync(int id);
    }
}
