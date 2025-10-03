using Luqma.API.Base;
using Luqma.Core.Features.MenuItems.Commands.Models;
using Luqma.Data.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Luqma.API.Areas.Manager.Controllers
{
    
    [ApiController]

    public class MenuItemsController:AppBaseController
    {
        [HttpPost(Router.ManagerMenuItemsRouting.Add)]
       public async Task<IActionResult> AddItem([FromForm]AddMenuItemCommand request)
        {
            var result = await mediator.Send(request);
            return Result(result);
        }
    }
}
