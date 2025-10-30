using Luqma.Core.Bases;
using MediatR;

namespace Luqma.Core.Features.KitchenRequirements.Queries.Models
{
    public class GetKitchenRequirementsQuery : IRequest<ApiResponse>
    {
        public int PageNumber { get; set; }
    }
}
