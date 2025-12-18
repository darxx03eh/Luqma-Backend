using Luqma.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Core.Features.OrderItems.Queries.Models
{
    public class GetOrderReportQuery : IRequest<FileApiResponse>
    {
        public int OrderId { get; set; }
        public GetOrderReportQuery(int orderId)
        {
            OrderId = orderId;
        }
    }
}
