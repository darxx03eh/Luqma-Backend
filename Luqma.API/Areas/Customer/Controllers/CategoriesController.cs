using Luqma.API.Base;
using Luqma.Core.Features.Categories.Queries.Models;
using Luqma.Data.Helpers;
using Luqma.Data.Routing;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Luqma.API.Areas.Customer.Controllers
{

    [ApiController]
    // [Authorize(Roles ="Customer")]
    public class CategoriesController : AppBaseController
    {
        [HttpGet(Router.CustomerCategoriesRouting.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var result = await mediator.Send(new GetAllCategoryQuery());
            return Result(result);

        }
        [HttpGet(Router.CustomerCategoriesRouting.GetById)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var result = await mediator.Send(new GetCategoryQuery(id));
            return Result(result);

        }
    }
}
