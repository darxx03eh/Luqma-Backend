using Luqma.API.Base;
using Luqma.Core.Features.Salaries.Commands.Models;
using Luqma.Core.Features.Salaries.Queries.Models;
using Luqma.Data.Helpers;
using Luqma.Data.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Luqma.API.Controllers
{
    [Authorize]
    [ApiController]
    public class SalariesController : AppBaseController
    {
        [Authorize(Roles = Roles.Finance)]

        [HttpPost(Router.SalariesRouting.GenerateSalaries)]
        public async Task<IActionResult> GenerateSalaries()
        {
            var result = await mediator.Send(new GenerateSalariesCommand());
            return Result(result);
        }
        [Authorize(Roles = Roles.Finance)]
        [HttpPost(Router.SalariesRouting.GenerateSalariesForUser)]
        public async Task<IActionResult> GenerateSalariesForUser(int id, int? year = 0, int? month = 0) 
        {
            var result = await mediator.Send(new GenerateSalaryForUserCommand()
            {
                Id = id,
                Year = year,
                Month = month
            });
            return Result(result);
        }
        [Authorize(Roles = $"{Roles.Manager},{Roles.Finance}")]
        [HttpGet(Router.SalariesRouting.GetSalaries)]
        public async Task<IActionResult> GetSalaries(string? name, string? status, int pageNumber = 1, int year = 0, int month = 0)
        {
            var result = await mediator.Send(new GetSalariesQuery()
            {
                PageNumber = pageNumber,
                Name = name,
                Status = status,
                Year = year,
                Month = month
            });
            return Result(result);
        }
        [Authorize(Roles = Roles.Finance)]
        [HttpDelete(Router.SalariesRouting.DeleteSalary)]
        public async Task<IActionResult> DeleteSalary(int id)
        {
            var result = await mediator.Send(new DeleteSalaryCommand(id));
            return Result(result);
        }
        [Authorize(Roles = Roles.Finance)]
        [HttpPatch(Router.SalariesRouting.ChangeSalaryStatus)]
        public async Task<IActionResult> ChangeSalaryStatus([FromBody] ChangeSalaryStatusCommand request)
        {
            var result = await mediator.Send(request);
            return Result(result);
        }
        [Authorize(Roles = Roles.Finance)]
        [HttpPatch(Router.SalariesRouting.ChangeSalaryAmount)]
        public async Task<IActionResult> ChangeSalaryAmount([FromBody] ChangeSalaryAmountCommand request)
        {
            var result = await mediator.Send(request);
            return Result(result);
        }
    }
}
