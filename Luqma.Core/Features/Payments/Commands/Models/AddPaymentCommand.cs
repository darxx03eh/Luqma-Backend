using Luqma.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Core.Features.Payments.Commands.Models
{
    public class AddPaymentCommand : IRequest<ApiResponse>
    {
        public int OrderId { get; set; }
        public string PaymentMethod { get; set; }

    }
}
