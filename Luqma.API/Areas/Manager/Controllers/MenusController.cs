using Luqma.API.Base;
using Luqma.Core.Features.MenuItems.Queries.Models;
using Luqma.Core.Features.Menus.Commands.Models;
using Luqma.Core.Features.Menus.Commands.Models;
using Luqma.Core.Features.Menus.Queries.Models;
using Luqma.Data.Helpers;
using Luqma.Data.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Luqma.API.Areas.Manager.Controllers
{
    
    [ApiController]
    [Authorize(Roles =Roles.Manager)]
    public class MenusController : AppBaseController
    {
        [HttpPost(Router.ManagerMenuRouting.Add)]
        public async Task<IActionResult> AddMenu([FromBody] AddMenuCommand request)
        {
            var result = await mediator.Send(request);
            return Result(result);
        }

      
    }
}
