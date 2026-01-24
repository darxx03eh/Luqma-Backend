using Luqma.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Core.Features.OrderItems.Commands.Models
{
   public  class UpdateQuantityForItemByCashierCommand:IRequest<ApiResponse>
    {
        public int ItemId { get; set; }
        public int OrderId { get; set; }
        public double Quantity { get; set; }


    }
}
