using Luqma.API.Base;
using Luqma.Core.Features.Customers.commands.Models;
using Luqma.Core.Features.Customers.Queries.Models;
using Luqma.Data.Helpers;
using Luqma.Data.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Luqma.API.Areas.Customer.Controllers
{
    [ApiController]
    public class CustomersController : AppBaseController
    {
        [HttpPost(Router.CustomerRouting.AddPhoneNumberThenSend)]
        public async Task<IActionResult> AddPhoneNumberThenSend(AddPhoneNumberCommand request)
        {
            var result = await mediator.Send(request);
            return Result(result);
        }

        [Authorize(Roles = Roles.Customer)]
        [HttpPut(Router.CustomerRouting.UpdateCustomerDetails)]
        public async Task<IActionResult> UpdateCustomer(UpdateCustomerDetailsCommand request)
        {
            var result = await mediator.Send(request);
            return Result(result);
        }

        [HttpPost(Router.CustomerRouting.ConfirmPhoneNumberCode)]
        public async Task<IActionResult> ConfimPhoneNumberCode(ConfirmPhoneNumberCodeCommand request)
        {
            var result = await mediator.Send(request);
            return Result(result);
        }

        [HttpGet(Router.CustomerRouting.GetCustomerInfo)]
        [Authorize(Roles = Roles.Customer)]
        public async Task<IActionResult> GetCustomerInfo()
        {
            var result = await mediator.Send(new GetCustomerInformationQuery());
            return Result(result);
        }
    }
}