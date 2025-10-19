using Luqma.Core.Bases;
using MediatR;

namespace Luqma.Core.Features.KitchenRequirments.Queries.Models
{
    public class GetKitchenRequirementsByIdQuery : IRequest<ApiResponse>
    {
        public int Id { get; set; }
        public GetKitchenRequirementsByIdQuery(int id) => Id = id;
    }
}
