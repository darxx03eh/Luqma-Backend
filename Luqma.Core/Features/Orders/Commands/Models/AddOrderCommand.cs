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
        public double TotalPrice { get; set; }
        public ICollection<(int id,int quantity)> items { get; set; } = new HashSet<(int,int)>();
         public int CustomerId { get; set; }
        public string PaymentMethode { get; set; }
        public bool isHoliday { get; set; }


    }
}
