using Luqma.API.Base;
using Luqma.Core.Features.Orders.Commands.Models;
using Luqma.Core.Features.Orders.Queries.Models;
using Luqma.Data.Helpers;
using Luqma.Data.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Luqma.API.Areas.Delivery.Controllers
{

    [ApiController]
    [Authorize(Roles =Roles.Delivery)]
    public class OrdersController : AppBaseController
    {
        [HttpGet(Router.DeliveryOrderRouting.ViewOrders)]
        public async Task<IActionResult> ViewOrders()
        {
            var result = await mediator.Send(new ViewOrdersByDeliveryQuery());
            return Result(result);
        }

        [HttpGet(Router.DeliveryOrderRouting.ViewOrderDetails)]
        
        public async Task<IActionResult> ViewOrderDetails([FromRoute]int id)
        {
            var result = await mediator.Send(new ViewOrderDetailsByDeliveryQuery(id));
            return Result(result);
        }
        [HttpPatch(Router.DeliveryOrderRouting.ChangeStatus)]
        public async Task<IActionResult> ChangeStatusToOutForDelivery([FromRoute]int id)
        {
            var result = await mediator.Send(new ChangeStatusToOutByDeliveryCommand(id));
            return Result(result);
        }
        [HttpGet(Router.DeliveryOrderRouting.viewOutOrders)]
        public async Task<IActionResult> viewOutOrders()
        {
            var result = await mediator.Send(new viewOutOrdersforDeliveryQuery());
            return Result(result);
        }
        [HttpPatch(Router.DeliveryOrderRouting.ChangeStatusToDelivered)]
        public async Task<IActionResult> ChangeStatusToDelivered([FromRoute]int id)
        {
            var result = await mediator.Send(new ChangeStatusByDeliveryToDeliveredCommand( id));
            return Result(result);
        }

    }
}
