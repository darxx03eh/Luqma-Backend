using Luqma.Core.Bases;
using Luqma.Core.Features.OrderItems.Commands.Models;
using Luqma.Core.ResponseKeys;
using Luqma.Service.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Core.Features.OrderItems.Commands.Handlers
{
    public class OrderItemCommandHandler : ApiResponseHandler,
        IRequestHandler<AddItemToOrderCommand, ApiResponse>,
        IRequestHandler<DeleteItemFromOrderCommand,ApiResponse>
    {
        private readonly IOrderItemService _orderItemService;

        public OrderItemCommandHandler(IOrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
        }
        public async Task<ApiResponse> Handle(AddItemToOrderCommand request, CancellationToken cancellationToken)
        {
          var result=  await _orderItemService.AddItemToOrderAsync(request.ItemId, request.Quantity);
            return result switch
            {
                "the item is added to order successfully" => Success(null, message:SharedResponseKeys.SuccessAddItemToOrder)
            };
        }

        public async Task<ApiResponse> Handle(DeleteItemFromOrderCommand request, CancellationToken cancellationToken)
        {
           var result= await _orderItemService.DeleteItemFromOrderAsync(request.ItemId);
            return result switch
            {
                "the item is deleted from order successfully" => Deleted(SharedResponseKeys.SuccessDeleteItemFromOrderByCashier)
            };
        }
    }
}
