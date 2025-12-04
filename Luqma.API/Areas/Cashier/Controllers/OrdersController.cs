using Luqma.API.Base;
using Luqma.Core.Features.Orders.Commands.Models;
using Luqma.Data.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Luqma.API.Areas.Cashier.Controllers
{
   
    [ApiController]
    [Authorize(Roles =Roles.Cashier)]
    public class OrdersController : AppBaseController
    {
        [HttpPost(Data.Routing.Router.CashierOrderRouting.AddOrder)]
        public async Task<IActionResult> AddOrder(placeOrderCommand request)
        {
            var result = await mediator.Send(request);
            return Result(result);
        }
       /* public async Task<IActionResult> UpdateOnOrderTotalPrice(UpdateOnOrderTotalPriceCommand request)
        {
            var result = await mediator.Send(request);
            return Result(result);
        }*/

    }
}
