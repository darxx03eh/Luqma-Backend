using Luqma.Core.Bases;
using MediatR;

namespace Luqma.Core.Features.KitchenRequirments.Queries.Models
{
    public class GetKitchenRequirementsQuery : IRequest<ApiResponse>
    {
        public int PageNumber { get; set; }
    }
}
