using Luqma.Core.Bases;
using Luqma.Core.Features.Orders.Commands.Models;
using Luqma.Core.ResponseKeys;
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
        IRequestHandler<AddOrderCommand,ApiResponse>
       
       
    {
        private readonly IOrderService _orderService;

        public OrderCommandHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<ApiResponse> Handle(AddOrderCommand request, CancellationToken cancellationToken)
        {

          var(orderid,result)=  await _orderService.AddOrderAsync(request.Note);
            return result switch
            {
                "the weather is unknown" => InternalServerError(SharedResponseKeys.UnKnownWeather),
                "the cart is empty" => NotFound(SharedResponseKeys.EmptyCart),
                "the order is added successfully" => Success(orderid, message: SharedResponseKeys.SuccessAddOrder)
            };

        }
    }
}
