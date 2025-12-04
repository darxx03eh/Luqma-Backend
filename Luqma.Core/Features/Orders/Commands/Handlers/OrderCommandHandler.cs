using Luqma.Core.Bases;
using Luqma.Core.Features.Orders.Commands.Models;
using Luqma.Core.ResponseKeys;
using Luqma.Data.Response.Order;
using Luqma.Service.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Core.Features.Orders.Commands.Handlers
{
    public class OrderCommandHandler : ApiResponseHandler,
        IRequestHandler<AddOrderCommand,ApiResponse>,
        IRequestHandler<placeOrderCommand,ApiResponse>
        //IRequestHandler<UpdateOnOrderTotalPriceCommand,ApiResponse>

       
       
    {
        private readonly IOrderService _orderService;

        public OrderCommandHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<ApiResponse> Handle(AddOrderCommand request, CancellationToken cancellationToken)
        {

          var(orderid,result)=  await _orderService.AddOrderAsync(request.Note);
            var OrderResponse = new OrderResponse()
            {
                OrderId = orderid
            };
            return result switch
            {
                "the weather is unknown" => InternalServerError(SharedResponseKeys.UnKnownWeather),
                "the cart is empty" => NotFound(SharedResponseKeys.EmptyCart),
                "the order is added successfully" => Success(OrderResponse, message: SharedResponseKeys.SuccessAddOrder)
            };

        }

        public  async Task<ApiResponse> Handle(placeOrderCommand request, CancellationToken cancellationToken)
        {
            var result = await _orderService.PlaceOrderAsync();
            return result switch
            {
                "the weather is unknown" => InternalServerError(SharedResponseKeys.UnKnownWeather),
                "the order is added by Cashier successfully" => Success(null, message: SharedResponseKeys.SuccessAddOrderByCashier)

            };
        }

       /* public async Task<ApiResponse> Handle(UpdateOnOrderTotalPriceCommand request, CancellationToken cancellationToken)
        {
            
        }*/
    }
}
