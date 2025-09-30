using Luqma.API.Base;
using Luqma.Core.Features.Users.Commands.Models;
using Luqma.Data.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Luqma.API.Controllers
{
    [Authorize]
    [ApiController]
    public class UsersController : AppBaseController
    {
        [HttpPost(Router.UsersRouting.ChangePassword)]
        public async Task<IActionResult> ChangePassowrd([FromBody] ChangePasswordCommand request)
        {
            var result = await mediator.Send(request);
            return Result(result);
        }
    }
}
