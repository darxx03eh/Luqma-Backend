using Luqma.API.Base;
using Luqma.Core.Features.MenuContains.Queries.Models;
using Luqma.Data.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Luqma.API.Areas.Customer.Controllers
{
  
    [ApiController]
    public class MenuContainsController : AppBaseController
    {
        [HttpGet(Router.MenuContainsRouting.GetItemsByMenuId)]
        public async Task<IActionResult> GetitemsByMenuId([FromRoute]int id)
        {
            var result = await mediator.Send(new GetitemsbyMenuIdQuery(id));
            return Result(result);
        }
    }
}
