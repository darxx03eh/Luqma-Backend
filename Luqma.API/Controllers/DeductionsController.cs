using Luqma.API.Base;
using Luqma.Core.Features.Deductions.commands.Models;
using Luqma.Data.Helpers;
using Luqma.Data.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Luqma.API.Controllers
{
    [Authorize(Roles = Roles.Finance)]
    [ApiController]
    public class DeductionsController : AppBaseController
    {
        [HttpPost(Router.DeductionsRouting.AddDeductionToUser)]
        public async Task<IActionResult> AddDeductionToUser([FromBody] AddDeductionToUserCommand request)
        {
            var result = await mediator.Send(request);
            return Result(result);
        }
        [HttpDelete(Router.DeductionsRouting.RemoveDeductionFromUser)]
        public async Task<IActionResult> RemoveDeductionFromUser(int id)
        {
            var result = await mediator.Send(new RemoveDeductionFromUserCommand(id));
            return Result(result);
        }
        [HttpPut(Router.DeductionsRouting.UpdateDeduction)]
        public async Task<IActionResult> UpdateDeduction([FromBody] UpdateDeductionCommand request)
        {
            var result = await mediator.Send(request);
            return Result(result);
        }
    }
}
