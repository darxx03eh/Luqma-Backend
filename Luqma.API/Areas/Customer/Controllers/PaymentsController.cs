using Luqma.API.Base;
using Luqma.Core.Features.Payments.Commands.Models;
using Luqma.Data.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Luqma.API.Areas.Customer.Controllers
{

    [ApiController]
    public class PaymentsController : AppBaseController
    {

        [HttpPost(Router.CustomerPaymentRouting.ProcessPayment)]
        [Authorize]
       
        public async Task<IActionResult> ProcessPayment([FromBody] AddPaymentCommand request)
        {
            var result = await mediator.Send(request);
            return Result(result);
        }
        [HttpGet(Router.CustomerPaymentRouting.SuccessPayment)]
        public async Task<IActionResult>SuccessPayment([FromQuery]int orderid)
        {
            var result = await mediator.Send(new SuccessPaymentCommand(orderid));
            return Result(result);
        }
    }
}
