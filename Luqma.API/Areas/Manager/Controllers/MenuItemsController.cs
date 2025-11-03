using Luqma.API.Base;
using Luqma.Core.Features.MenuItems.Commands.Models;
using Luqma.Core.Features.MenuItems.Queries.Models;
using Luqma.Data.Helpers;
using Luqma.Data.Routing;
using Luqma.Infrastructure.Migrations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Luqma.API.Areas.Manager.Controllers
{

    [ApiController]
    [Authorize(Roles = Roles.Manager)]
    public class MenuItemsController : AppBaseController
    {
        [HttpPost(Router.ManagerMenuItemsRouting.Add)]
        public async Task<IActionResult> AddMenuItem([FromForm] AddMenuItemCommand request)
        {
            var result = await mediator.Send(request);
            return Result(result);
        }

        [HttpDelete(Router.ManagerMenuItemsRouting.Delete)]
        public async Task<IActionResult> DeleteMenuItem([FromRoute] int id)
        {
            var result = await mediator.Send(new DeleteMenuItemCommand(id));
            return Result(result);
        }
        [HttpPut(Router.ManagerMenuItemsRouting.Update)]
        public async Task<IActionResult> UpdateMenuItem([FromForm]UpdateMenuItemCommand request)
        {

            var result = await mediator.Send(request);
            return Result(result);
        }
        [HttpGet(Router.MenuItemRouting.GetAll)]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var result = await mediator.Send(new GetAllMenuItemQuery());
            return Result(result);

        }
        [HttpGet(Router.ManagerMenuItemsRouting.GetById)]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            var result = await mediator.Send(new GetMenuItemByIdQuery(id));
            return Result(result);
        }


    }
}

