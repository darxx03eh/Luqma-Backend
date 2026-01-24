using Luqma.Core.Bases;
using MediatR;

namespace Luqma.Core.Features.KitchenItems.Queries.Models
{
    public class GetKitchenItemsQuery : IRequest<ApiResponse>
    {
        public int PageNumber { get; set; }
        public string? Search { get; set; }
    }
}
