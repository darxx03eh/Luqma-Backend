using Luqma.Core.Bases;
using Luqma.Data.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Core.Features.Orders.Commands.Models
{
   public  class AddOrderCommand :IRequest<ApiResponse>
    {
     public string? Note { get; set; }


    }
}
