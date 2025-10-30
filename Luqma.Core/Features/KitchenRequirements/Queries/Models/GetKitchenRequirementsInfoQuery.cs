using Luqma.Core.Bases;
using MediatR;

namespace Luqma.Core.Features.KitchenRequirements.Queries.Models
{
    public class GetKitchenRequirementsInfoQuery : IRequest<ApiResponse>
    {
        public int Id { get; set; }
        public GetKitchenRequirementsInfoQuery(int id) => Id = id;
    }
}
