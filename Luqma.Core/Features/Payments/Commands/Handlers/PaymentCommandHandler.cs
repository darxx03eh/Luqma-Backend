using Luqma.Core.Bases;
using Luqma.Core.Features.Payments.Commands.Models;
using Luqma.Core.ResponseKeys;
using Luqma.Service.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Core.Features.Payments.Commands.Handlers
{
    public class PaymentCommandHandler : ApiResponseHandler,
        IRequestHandler<AddPaymentCommand, ApiResponse>,
        IRequestHandler<SuccessPaymentCommand, ApiResponse>


    {

        private readonly IPaymentService _paymentService;

        public PaymentCommandHandler(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        public async Task<ApiResponse> Handle(AddPaymentCommand request, CancellationToken cancellationToken)
        {
            var (url, result) = await _paymentService.ProcessPaymentAsync(request.OrderId, request.PaymentMethod);
            return result switch
            {
                "the cart is empty" => NotFound(SharedResponseKeys.EmptyCart),
                "the process payment by visa is done" => Success(url, message: SharedResponseKeys.SuccessProcessPaymentByVisa),
                "the process payment by cash is done" => Success(null, message: SharedResponseKeys.SuccessProcessPaymentByCash),
                "the payment method is not exist in our website" => NotFound(SharedResponseKeys.NotExistPaymentMethod)
            };

        }

        public async Task<ApiResponse> Handle(SuccessPaymentCommand request, CancellationToken cancellationToken)
        {
          var result= await  _paymentService.SuccessPaymentByVisaAsync(request.Id);
            return result switch
            {
                "the payment by visa is success" => Success(null, message: SharedResponseKeys.SuccessPaymentByVisa)
            };

        }
     }

    }
}

