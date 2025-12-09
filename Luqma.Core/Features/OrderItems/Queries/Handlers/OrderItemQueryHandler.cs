using AutoMapper;
using Luqma.Core.Bases;
using Luqma.Core.Features.OrderItems.Queries.Models;
using Luqma.Core.ResponseKeys;
using Luqma.Data.Response.Order;
using Luqma.Service.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Core.Features.OrderItems.Queries.Handlers
{
    public class OrderItemQueryHandler : ApiResponseHandler,
        IRequestHandler<ViewOrdersQuery, ApiResponse>,
        IRequestHandler<ViewOrderDetailsQuery,ApiResponse>
    {
        private readonly IOrderItemService _orderItemService;
        private readonly IMapper _mapper;

        public OrderItemQueryHandler(IOrderItemService orderItemService,IMapper mapper)
        {
            _orderItemService = orderItemService;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(ViewOrdersQuery request, CancellationToken cancellationToken)
        {
           var (orders,result)= await _orderItemService.GetOrdersAsync();
            var ordersRe=_mapper.Map<List<ViewOrderResponse>>(orders);
            return result switch
            {
                "the orders is fetched successfully" => Success(ordersRe,message:SharedResponseKeys.SuccessViewOrders)
            };
            
        }

        public async Task<ApiResponse> Handle(ViewOrderDetailsQuery request, CancellationToken cancellationToken)
        {
            var (orderitems, result) = await _orderItemService.GetOrderDetailsAsync(request.orderid);
            var OrderDetailsRe=_mapper.Map<List<ViewOrderDetailsResponse>>(orderitems);
            return result switch
            {
                "the order details is fetched successfully" => Success(OrderDetailsRe, message: SharedResponseKeys.SuccessFetchOrderDetails)
            };
        }
    }
}
