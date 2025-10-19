using Luqma.Data.DTOs.RequirmentItems;
using Luqma.Data.Response.KitchenRequirments;
using Luqma.Data.Wrappers;

namespace Luqma.Service.Interfaces
{
    public interface IKitchenRequirmentsService
    {
        public Task<string> PlaceNewKitchenRequirmentsAsync(string? note, IList<RequirmentItemsDTO> requirmentItems);
        public Task<string> ChangeKitchenRequirmentsAsync(int id, string status);
        public Task<(string, PaginatedResult<GetKitchenRequirmentsResponse>?)> GetKitchenRequirmentsAsync(int pageNumber);
        public Task<(string, GetKitchenRequirmentsResponse?)> GetKitchenRequirmentsByIdAsync(int id);
        public Task<string> DeleteKitchenRequirmentsAsync(int id);
        public Task<(string, GetKitchenRequirmentsInfoResponse?)> GetKitchenRequirmentsInfoAsync(int id);
    }
}
