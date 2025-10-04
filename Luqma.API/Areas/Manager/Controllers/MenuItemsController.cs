using Luqma.API.Base;
using Luqma.Core.Features.MenuItems.Commands.Models;
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
        [HttpPatch(Router.ManagerMenuItemsRouting.Update)]
        public async Task<IActionResult> UpdateMenuItem([FromForm]UpdateMenuItemCommand request)
        {

            var result = await mediator.Send(request);
            return Result(result);
        }


    }
}

