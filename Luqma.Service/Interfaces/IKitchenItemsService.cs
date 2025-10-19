using Luqma.Data.Response.KitchenItems;
using Luqma.Data.Wrappers;
using Microsoft.AspNetCore.Http;

namespace Luqma.Service.Interfaces
{
    public interface IKitchenItemsService
    {
        public Task<(string, PaginatedResult<GetKitchenItemsResponse>?)> GetPaginatedKitchenItemsAsync(int pageNumber, string? search);
        public Task<(string, GetKitchenItemsResponse?)> AddKitchenItemAsync(
            string item, string status, IFormFile? image, string? note, string unit, double quantity);
        public Task<(string, GetKitchenItemsResponse?)> UpdateKitchenItemAsync(int id,
            string item, string status, IFormFile? image, string? note, string unit, double quantity);
        public Task<(string, GetKitchenItemsResponse?)> GetKitchenItemByIdAsync(int id);
        public Task<string> DeleteKitchenItemAsync(int id);
        public Task<string> UpdateItemStatusAsync(int id, string status);
    }
}
