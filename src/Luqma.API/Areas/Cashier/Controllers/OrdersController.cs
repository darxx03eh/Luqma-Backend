using Luqma.API.Base;
using Luqma.Core.Features.Orders.Commands.Models;
using Luqma.Data.Helpers;
using Luqma.Data.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Luqma.API.Areas.Cashier.Controllers
{
   
    [ApiController]
    [Authorize(Roles =Roles.Cashier)]
    public class OrdersController : AppBaseController
    {
        [HttpPost(Router.CashierOrderRouting.AddOrder)]
        public async Task<IActionResult> AddOrder(placeOrderCommand request)
        {
            var result = await mediator.Send(request);
            return Result(result);
        }
        [HttpPatch(Router.CashierOrderRouting.SubmitOrder)]
        public async Task<IActionResult> UpdateOnOrderTotalPrice(UpdateOnOrderTotalPriceCommand request)
        {
            var result = await mediator.Send(request);
            return Result(result);
        }
        [HttpDelete(Router.CashierOrderRouting.CancelOrder)]
        public async Task<IActionResult> CancelOrder()
        {
            var result = await mediator.Send(new CancelOrderByCashierCommand());
            return Result(result);
        }
        [HttpDelete(Router.CashierOrderRouting.CancelOrderById)]
        public async Task<IActionResult> CancelOrder([FromRoute] int id)
        {
            var result = await mediator.Send(new CancelOrder(id));
            return Result(result);
        }
        [HttpPatch(Router.CashierOrderRouting.UpdateOrder)]
        public async Task<IActionResult> UpdateOrder(UpdateOrderByCashierCommand request)
        {
            var result = await mediator.Send(request);
            return Result(result);
        }
       

    }
}
