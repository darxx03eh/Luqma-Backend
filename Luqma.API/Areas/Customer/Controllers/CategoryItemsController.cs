using Luqma.API.Base;
using Luqma.Core.Features.CategoryItems.Queries.Models;
using Luqma.Data.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Luqma.Core.Bases;

namespace Luqma.API.Areas.Customer.Controllers
{
   
    [ApiController]
    
    public class CategoryItemsController :AppBaseController
    {
        [HttpGet(Router.CategoryItemsRouting.GetItemsByCategoryId)]
        public async Task<IActionResult> GetItemsByCategoryId([FromRoute] int id)
        {
            var result =await  mediator.Send(new GetItemsbyCategoryIdQuery(id));
            return Result(result);
        }


    }
}
