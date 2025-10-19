using Luqma.Core.Bases;
using Luqma.Data.DTOs.RequirmentItems;
using MediatR;

namespace Luqma.Core.Features.KitchenRequirments.Commands.Models
{
    public class PlaceNewKitchenRequirmentsCommand : IRequest<ApiResponse>
    {
        public string? Note { get; set; }
        public IList<RequirmentItemsDTO> RequirmentItems { get; set; }
    }
}
