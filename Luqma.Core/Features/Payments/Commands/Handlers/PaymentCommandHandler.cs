using Luqma.Core.Bases;
using Luqma.Core.Features.Payments.Commands.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Core.Features.Payments.Commands.Handlers
{
    public class PaymentCommandHandler : ApiResponseHandler


    {
         public async Task<ApiResponse> Handle(AddPaymentCommand request, CancellationToken cancellationToken)
         {

         }
     }
    }
}
