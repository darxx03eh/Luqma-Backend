using Luqma.API.Base;
using Luqma.Core.Features.OrderItems.Commands.Models;
using Luqma.Core.Features.OrderItems.Queries.Models;
using Luqma.Data.Helpers;
using Luqma.Data.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Luqma.API.Areas.Cashier.Controllers
{

    [ApiController]
    [Authorize(Roles = Roles.Cashier)]
    public class OrderItemsController : AppBaseController
    {
        [HttpPost(Router.CashierOrderItemRouting.AddItemToOrder)]
        public async Task<IActionResult> AddItemToOrder(AddItemToOrderCommand request)
        {
            var result = await mediator.Send(request);
            return Result(result);
        }
        [HttpDelete(Router.CashierOrderItemRouting.DeleteItemFromOrder)]
        public async Task<IActionResult> DelteItemFromOrder([FromRoute]int id)
        {
            var result = await mediator.Send(new DeleteItemFromOrderCommand(id));
            return Result(result);
        }
        [HttpGet(Router.CashierOrderItemRouting.ViewOrders)]
        public async Task<IActionResult> ViewOrders()
        {
            var result = await mediator.Send(new ViewOrdersQuery());
            return Result(result);
        }
        [HttpGet(Router.CashierOrderItemRouting.GetOrderDetailsByID)]
        [AllowAnonymous]
        [Authorize(Roles = $"{Roles.Cashier},{Roles.Chef}")]
        public async Task<IActionResult> ViewOrderDetails([FromRoute]int id)
        {
            var result = await mediator.Send(new ViewOrderDetailsQuery(id));
            return Result(result);
        }
        [HttpPatch(Router.CashierOrderItemRouting.UpdateQuantity)]
        public async Task<IActionResult> UpdateQuantityForItemByCashier(UpdateQuantityForItemByCashierCommand request)
        {
            var result = await mediator.Send(request);
            return Result(result);
        }

    }
}

