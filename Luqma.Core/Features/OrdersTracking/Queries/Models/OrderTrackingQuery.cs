using Luqma.Core.Bases;
using MediatR;

namespace Luqma.Core.Features.OrdersTracking.Queries.Models
{
    public class OrderTrackingQuery : IRequest<ApiResponse>
    {
        public int Id { get; set; }

        public OrderTrackingQuery(int id) => Id = id;
    }
}