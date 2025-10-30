using Luqma.Data.DTOs.RequirementItems;
using Luqma.Data.Response.KitchenRequirements;
using Luqma.Data.Wrappers;

namespace Luqma.Service.Interfaces
{
    public interface IKitchenRequirementsService
    {
        public Task<(string, GetKitchenRequirementsResponse?)> PlaceNewKitchenRequirementsAsync(string? note, IList<RequirementItemsDTO> requirmentItems);
        public Task<string> ChangeKitchenRequirementsAsync(int id, string status);
        public Task<(string, PaginatedResult<GetKitchenRequirementsResponse>?)> GetKitchenRequirementsAsync(int pageNumber);
        public Task<(string, GetKitchenRequirementsResponse?)> GetKitchenRequirementsByIdAsync(int id);
        public Task<string> DeleteKitchenRequirementsAsync(int id);
        public Task<(string, GetKitchenRequirementsInfoResponse?)> GetKitchenRequirementsInfoAsync(int id);
    }
}
