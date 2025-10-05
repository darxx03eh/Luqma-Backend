using Luqma.API.Base;
using Luqma.Core.Features.Menus.Queries.Models;
using Luqma.Data.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Luqma.API.Areas.Customer.Controllers
{
   
    [ApiController]
    public class MenusController :AppBaseController
    {
      

        [HttpGet(Router.MenuRouting.GetAll)]
        public async Task<IActionResult> GetAllMenus()
        {
            var result = await mediator.Send(new GetAllMenusQuery());
            return Result(result);
        }

       
            
    }
}
