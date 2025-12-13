using AutoMapper;
using Luqma.Core.Bases;
using Luqma.Core.Features.Orders.Queries.Models;
using Luqma.Core.ResponseKeys;
using Luqma.Data.Response.Order;
using Luqma.Service.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Core.Features.Orders.Queries.Handlers
{
    public class OrderQueryHandler : ApiResponseHandler,
         IRequestHandler<ViewOrdersByChefQuery, ApiResponse>
    {
        private readonly IMapper _mapper;
        private readonly IOrderService _orderService;

        public OrderQueryHandler(IMapper mapper,IOrderService orderService)
        {
            _mapper = mapper;
            _orderService = orderService;
        }
        public async Task<ApiResponse> Handle(ViewOrdersByChefQuery request, CancellationToken cancellationToken)
        {
            var (orders, result) = await _orderService.getOrdersForChefAsync();
           var ordersRe= _mapper.Map<List<ViewOrderResponse>>(orders);
            return result switch
            {
                "the orders is fetched successfully" => Success(ordersRe, message: SharedResponseKeys.SuccessViewOrders)
            };
        }
    }
}
