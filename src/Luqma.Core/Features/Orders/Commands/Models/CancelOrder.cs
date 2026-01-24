using Luqma.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Core.Features.Orders.Commands.Models
{
    public class CancelOrder : IRequest<ApiResponse>
    {
        public int OrderId { get; set; }
        public CancelOrder(int orderId)
        {
            OrderId = orderId;
        }
    }
}
