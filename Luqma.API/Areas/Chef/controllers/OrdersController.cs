using CloudinaryDotNet.Actions;
using Luqma.API.Base;
using Luqma.Core.Features.Orders.Commands.Models;
using Luqma.Core.Features.Orders.Queries.Models;
using Luqma.Data.Helpers;
using Luqma.Data.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Luqma.API.Areas.Chef.controllers
{
  
    [ApiController]
    [Authorize(Roles =Roles.Chef)]
    public class OrdersController : AppBaseController
    {
        [HttpGet(Router.ChefOrderRouting.ViewOrders)]
        public async Task<IActionResult> ViewOrders()
        {
            var result = await mediator.Send(new ViewOrdersByChefQuery());
            return Result(result);
        }
        [HttpPatch(Router.ChefOrderRouting.ChangeStatus)]
        public async Task<IActionResult> ChangeStatus([FromRoute]int id)
        {
            var result = await mediator.Send(new ChangeStatusByChefCommand(id));
            return Result(result);
        }
    }
}
