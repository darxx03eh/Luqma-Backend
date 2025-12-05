using Luqma.API.Base;
using Luqma.Core.Features.Orders.Commands.Models;

using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Luqma.Data.Routing;
using Microsoft.AspNetCore.Authorization;
using Luqma.Data.Helpers;



namespace Luqma.API.Areas.Customer.Controllers
{

    [ApiController]
    [Authorize]
    public class OrdersController : AppBaseController
    {
        [HttpPost(Data.Routing.Router.CustomerOrderRouting.AddOrder)]
    public async Task<IActionResult> AddOrder(AddOrderCommand request)
        {
            var result = await mediator.Send(request);
            return Result(result);
        }

        [HttpDelete(Data.Routing.Router.CustomerOrderRouting.CancelOrder)]
       
        public async Task<IActionResult> CancelOrder([FromRoute] int id)
        {
            var result = await mediator.Send(new CancelOrder(id));
            return Result(result);
        }   

    }
}
