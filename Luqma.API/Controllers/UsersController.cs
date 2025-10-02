using Luqma.API.Base;
using Luqma.Core.Features.Users.Commands.Models;
using Luqma.Core.Features.Users.Queries.Models;
using Luqma.Data.Helpers;
using Luqma.Data.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Luqma.API.Controllers
{
    [Authorize]
    [ApiController]
    public class UsersController : AppBaseController
    {
        [HttpPatch(Router.UsersRouting.ChangePassword)]
        public async Task<IActionResult> ChangePassowrd([FromBody] ChangePasswordCommand request)
        {
            var result = await mediator.Send(request);
            return Result(result);
        }
        [HttpPatch(Router.UsersRouting.ChangeName)]
        public async Task<IActionResult> ChangeName([FromBody] ChangeNameCommand request)
        {
            var result = await mediator.Send(request);
            return Result(result);
        }
        [HttpPatch(Router.UsersRouting.UploadProfileImage)]
        public async Task<IActionResult> UploadProfileImage([FromForm] UploadProfileImageCommand request)
        {
            var result = await mediator.Send(request);
            return Result(result);
        }
        [HttpPatch(Router.UsersRouting.ChangeUserName)]
        public async Task<IActionResult> ChangeUserName([FromBody] ChangeUserNameCommand request)
        {
            var result = await mediator.Send(request);
            return Result(result);
        }
        [HttpDelete(Router.UsersRouting.DeleteProfileImage)]
        public async Task<IActionResult> DeleteProfileImage()
        {
            var result = await mediator.Send(new DeleteProfileImageCommand());
            return Result(result);
        }
        [HttpPatch(Router.UsersRouting.ChangeBirthDate)]
        public async Task<IActionResult> ChangeBirthDate([FromBody] ChangeBirthDateCommand request)
        {
            var result = await mediator.Send(request);
            return Result(result);
        }
        [Authorize(Roles = Roles.Manager)]
        [HttpDelete(Router.UsersRouting.DeactiveUser)]
        public async Task<IActionResult> DeactiveUser([FromBody] DeactivateUserCommand request)
        {
            var result = await mediator.Send(request);
            return Result(result);
        }
        [Authorize(Roles = Roles.Manager)]
        [HttpPost(Router.UsersRouting.ActivateUser)]
        public async Task<IActionResult> ActivateUser([FromBody] ActivateUserCommand request)
        {
            var result = await mediator.Send(request);
            return Result(result);
        }
        [Authorize(Roles = Roles.Manager)]
        [HttpGet(Router.UsersRouting.ViewUsers)]
        public async Task<IActionResult> ViewUsers(int pageNumber = 1)
        {
            var result = await mediator.Send(new ViewUsersCommand() { PageNumber = pageNumber });
            return Result(result);
        }
    }
}
