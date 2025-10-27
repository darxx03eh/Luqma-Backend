//using Luqma.API.Base;
//using Luqma.Core.Features.Payments.Commands.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace Luqma.API.Areas.Customer.Controllers
//{
    
//    [ApiController]
//    public class PaymentsController : AppBaseController
//    {
//        public async Task<IActionResult> ProcessPayment([FromBody]AddPaymentCommand request)
//        {
//            var result = await mediator.Send(request);
//            return Result(result);
//        }
//    }
//}
