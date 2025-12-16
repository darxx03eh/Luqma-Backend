using AutoMapper;
using Luqma.Core.Bases;
using Luqma.Core.Features.Orders.Commands.Models;
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
         IRequestHandler<ViewOrdersByChefQuery, ApiResponse>,
        IRequestHandler<ViewOrdersByDeliveryQuery,ApiResponse>,
        IRequestHandler<ViewOrderDetailsByDeliveryQuery,ApiResponse>,
        IRequestHandler<viewOutOrdersforDeliveryQuery,ApiResponse>
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

        public async Task<ApiResponse> Handle(ViewOrdersByDeliveryQuery request, CancellationToken cancellationToken)
        {
            var (orders, result) = await _orderService.getOrdersForDeliveryAsync();
            var ordersRe = _mapper.Map<List<ViewOrderResponse>>(orders);
            return result switch
            {
                "the orders is fetched successfully" => Success(ordersRe, message: SharedResponseKeys.SuccessViewOrders)
            };

        }

        public async Task<ApiResponse> Handle(ViewOrderDetailsByDeliveryQuery request, CancellationToken cancellationToken)
        {
           var (OrderDetailsRe,result)= await _orderService.getOrderDetailsForDeliveryAsync(request.orderid);
            return result switch
            {
                "the order details is fetched successfully" => Success(OrderDetailsRe, message:SharedResponseKeys.SuccessFetchOrderDetails)
            };


        }
        public async Task<ApiResponse> Handle(viewOutOrdersforDeliveryQuery request, CancellationToken cancellationToken)
        {
           var(ordersRe,result)= await _orderService.getOutOrdersForDeliveryAsync();
           
            return result switch
            {
                "the orders is fetched successfully" => Success(ordersRe, message: SharedResponseKeys.SuccessViewOrders)
            };
        }
    }
}
