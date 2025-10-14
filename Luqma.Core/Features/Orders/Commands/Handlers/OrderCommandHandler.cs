using Luqma.Core.Bases;
using Luqma.Core.Features.Orders.Commands.Models;
using Luqma.Service.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Core.Features.Orders.Commands.Handlers
{
    public class OrderCommandHandler : ApiResponseHandler
       
    {
        private readonly IOrderService _orderService;

        public OrderCommandHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }
      
    }
}
