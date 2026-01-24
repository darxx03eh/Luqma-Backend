using Luqma.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Core.Features.Customers.commands.Models
{
   public class ConfirmPhoneNumberCodeCommand :IRequest<ApiResponse>
    {
        public string PhoneNumber { get; set; }
        public string Code { get; set; }

    }
}
