using Luqma.Core.Bases;
using MediatR;

namespace Luqma.Core.Features.KitchenItems.Queries.Models
{
    public class GetKitchenItemByIdQuery : IRequest<ApiResponse>
    {
        public int Id { get; set; }
        public GetKitchenItemByIdQuery(int id) => Id = id;
    }
}
