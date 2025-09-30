using Luqma.API.Base;
using Luqma.Core.Features.Categories.Queries.Models;
using Luqma.Data.Routing;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Luqma.API.Areas.Customer.Controllers
{

    [ApiController]
    public class CategoriesController : AppBaseController
    {
        [HttpGet(Router.CustomerCategoriesRouting.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var result = await mediator.Send(new GetAllCategoryQuery());
            return Result(result);

        }
    }
}
