using Luqma.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Core.Features.Orders.Queries.Models
{
    public class ViewOrderDetailsByDeliveryQuery : IRequest<ApiResponse>
    {
        public int orderid { get; set; }
        public ViewOrderDetailsByDeliveryQuery(int orderid)
        {
            this.orderid = orderid;
        }
    }
}
