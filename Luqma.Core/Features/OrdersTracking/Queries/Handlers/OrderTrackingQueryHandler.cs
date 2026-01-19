using Luqma.Core.Bases;
using Luqma.Core.Features.OrdersTracking.Queries.Models;
using Luqma.Core.ResponseKeys;
using Luqma.Service.Interfaces;
using MediatR;

namespace Luqma.Core.Features.OrdersTracking.Queries.Handlers
{
    public class OrderTrackingQueryHandler : ApiResponseHandler
        , IRequestHandler<OrderTrackingQuery, ApiResponse>
    {
        private readonly IOrderTrackingService orderTrackingService;

        public OrderTrackingQueryHandler(IOrderTrackingService orderTrackingService) => this.orderTrackingService = orderTrackingService;

        public async Task<ApiResponse> Handle(OrderTrackingQuery request, CancellationToken cancellationToken)
        {
            var (result, tracking) = await orderTrackingService.OrderTrackingAsync(request.Id);
            return result switch
            {
                "CustomerNotFound" => NotFound(SharedResponseKeys.CustomerNotFound),
                "OrderNotFound" => NotFound(SharedResponseKeys.OrderNotFound),
                "ThisOrderDoNotBelongToYou" => Forbidden(SharedResponseKeys.ThisOrderDoNotBelongToYou),
                "OrderTrackingFailed" => InternalServerError(SharedResponseKeys.OrderTrackingFailed),
                "TheOrderWasFoundAndTrackingTheOrder" =>
                Success(tracking, message: SharedResponseKeys.TheOrderWasFoundAndTrackingTheOrder),
                _ => InternalServerError(SharedResponseKeys.OrderTrackingFailed)
            };
        }
    }
}