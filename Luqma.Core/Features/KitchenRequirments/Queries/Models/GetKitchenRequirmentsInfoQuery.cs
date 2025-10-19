using Luqma.Core.Bases;
using MediatR;

namespace Luqma.Core.Features.KitchenRequirments.Queries.Models
{
    public class GetKitchenRequirmentsInfoQuery : IRequest<ApiResponse>
    {
        public int Id { get; set; }
        public GetKitchenRequirmentsInfoQuery(int id) => Id = id;
    }
}
