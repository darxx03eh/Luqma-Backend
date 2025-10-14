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
        public ICollection<int> ProductId { get; set; } = new HashSet<int>();
        public string Type { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender gender { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Street { get; set; }
        public string PhoneNumber { get; set; }

    }
}
