using Luqma.API.Base;
using Luqma.Core.Features.Salaries.Commands.Models;
using Luqma.Core.Features.Salaries.Queries.Models;
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
        [HttpDelete(Router.SalariesRouting.DeleteSalary)]
        public async Task<IActionResult> DeleteSalary(int id)
        {
            var result = await mediator.Send(new DeleteSalaryCommand(id));
            return Result(result);
        }
    }
}
