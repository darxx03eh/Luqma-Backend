using Luqma.Data.DTOs.RequirmentItems;

namespace Luqma.Service.Interfaces
{
    public interface IKitchenRequirmentsService
    {
        public Task<string> PlaceNewKitchenRequirmentsAsync(string? note, IList<RequirmentItemsDTO> requirmentItems);
    }
}
