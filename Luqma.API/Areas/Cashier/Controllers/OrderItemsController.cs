using Luqma.API.Base;
using Luqma.Core.Features.OrderItems.Commands.Models;
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
     }

    }

