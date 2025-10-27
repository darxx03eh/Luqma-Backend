using Luqma.API.Base;
using Luqma.Core.Features.Orders.Commands.Models;

using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Luqma.Data.Routing;



namespace Luqma.API.Areas.Customer.Controllers
{

    [ApiController]
    public class OrdersController : AppBaseController
    {
        [HttpPost(Data.Routing.Router.CustomerOrderRouting.AddOrder)]
    public async Task<IActionResult> AddOrder(AddOrderCommand request)
        {
            var result = await mediator.Send(request);
            return Result(result);
        }
            

    }
}
