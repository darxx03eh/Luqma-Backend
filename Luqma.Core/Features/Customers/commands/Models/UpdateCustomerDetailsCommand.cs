using Luqma.Core.Bases;
using Luqma.Data.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Luqma.Core.Features.Customers.commands.Models
{
   public class UpdateCustomerDetailsCommand :IRequest<ApiResponse>
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender gender { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Street { get; set; }
        public string PhoneNumber { get; set; }

    }
}
