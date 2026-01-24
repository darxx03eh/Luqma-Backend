using Luqma.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Core.Features.Orders.Commands.Models
{
    public class UpdateOrderByCashierCommand:IRequest<ApiResponse>
    {
        public int Id { get; set; }
        public string? Note { get; set; }
       
    }
}
