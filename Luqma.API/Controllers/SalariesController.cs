using Luqma.API.Base;
using Luqma.Core.Features.Salaries.Commands.Models;
using Luqma.Data.Helpers;
using Luqma.Data.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Luqma.API.Controllers
{
    [Authorize(Roles = Roles.Finance)]
    [ApiController]
    public class SalariesController : AppBaseController
    {
        [HttpPost(Router.SalariesRouting.GenerateSalaries)]
        public async Task<IActionResult> GenerateSalaries()
        {
            var result = await mediator.Send(new GenerateSalariesCommand());
            return Result(result);
        }
    }
}
