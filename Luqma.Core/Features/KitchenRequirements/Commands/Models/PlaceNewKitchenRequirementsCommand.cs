using Luqma.Core.Bases;
using Luqma.Data.DTOs.RequirementItems;
using MediatR;

namespace Luqma.Core.Features.KitchenRequirements.Commands.Models
{
    public class PlaceNewKitchenRequirementsCommand : IRequest<ApiResponse>
    {
        public string? Note { get; set; }
        public IList<RequirementItemsDTO> RequirementItems { get; set; }
    }
}
