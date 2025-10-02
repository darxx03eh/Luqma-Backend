using Luqma.API.Base;
using Luqma.Core.Features.UsersManagements.Commands.Models;
using Luqma.Data.Helpers;
using Luqma.Data.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Luqma.API.Controllers
{
    [Authorize(Roles = Roles.Manager)]
    [ApiController]
    public class UsersManagementsController : AppBaseController
    {
        [HttpDelete(Router.UsersRouting.DeactiveUser)]
        public async Task<IActionResult> DeactiveUser([FromBody] DeactivateUserCommand request)
        {
            var result = await mediator.Send(request);
            return Result(result);
        }
        [HttpPost(Router.UsersRouting.ActivateUser)]
        public async Task<IActionResult> ActivateUser([FromBody] ActivateUserCommand request)
        {
            var result = await mediator.Send(request);
            return Result(result);
        }
    }
}
