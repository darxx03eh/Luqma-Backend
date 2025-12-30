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
        [HttpGet(Router.UsersRouting.Profile)]
        public async Task<IActionResult> Profile(string username)
        {
            var result = await mediator.Send(new GetUserProfileQuery(username));
            return Result(result);
        }

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

        [HttpPost(Router.UsersRouting.UserAddress)]
        public async Task<IActionResult> AddUserAddress([FromBody] AddUserAddressCommand request)
        {
            var result = await mediator.Send(request);
            return Result(result);
        }

        [HttpPut(Router.UsersRouting.UserAddress)]
        public async Task<IActionResult> UpdateUserAddress([FromBody] UpdateUserAddressCommand request)
        {
            var result = await mediator.Send(request);
            return Result(result);
        }

        [HttpDelete(Router.UsersRouting.DeleteUserAddress)]
        public async Task<IActionResult> DeleteUserAddress(int id)
        {
            var result = await mediator.Send(new DeleteUserAddressCommand(id));
            return Result(result);
        }

        [HttpGet(Router.UsersRouting.ShowUserAddresses)]
        public async Task<IActionResult> ShowUserAddresses(int pageNumber = 1)
        {
            var result = await mediator.Send(new ShowUserAddressesQuery() { PageNumber = pageNumber });
            return Result(result);
        }

        [HttpGet(Router.UsersRouting.ShowSpecificAddress)]
        public async Task<IActionResult> ShowSpecificAddress(int id)
        {
            var result = await mediator.Send(new ViewSpecificAddressQuery(id));
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
            var result = await mediator.Send(new ViewUsersQuery() { PageNumber = pageNumber });
            return Result(result);
        }

        [Authorize(Roles = Roles.Manager)]
        [HttpPatch(Router.UsersRouting.ChangeUserRoles)]
        public async Task<IActionResult> ChangeUserRoles([FromBody] ChangeUserRolesCommand request)
        {
            var result = await mediator.Send(request);
            return Result(result);
        }

        [Authorize(Roles = Roles.Manager)]
        [HttpPut(Router.UsersRouting.UpdateUserData)]
        public async Task<IActionResult> UpdateUserData([FromBody] UpdateUserDataCommand request)
        {
            var result = await mediator.Send(request);
            return Result(result);
        }

        [Authorize(Roles = Roles.Manager)]
        [HttpPatch(Router.UsersRouting.ChangePasswordForUserByManager)]
        public async Task<IActionResult> ChangePasswordForUserByManager([FromBody] ChangePasswordForUserByManagerCommand request)
        {
            var result = await mediator.Send(request);
            return Result(result);
        }

        [Authorize(Roles = $"{Roles.Finance},{Roles.Manager}")]
        [HttpPatch(Router.UsersRouting.ChangeSalary)]
        public async Task<IActionResult> ChangeSalary([FromBody] ChangeSalaryCommand request)
        {
            var result = await mediator.Send(request);
            return Result(result);
        }

        [Authorize(Roles = $"{Roles.Finance}")]
        [HttpGet(Router.UsersRouting.DropdownUsers)]
        public async Task<IActionResult> DropdownUsers()
        {
            var result = await mediator.Send(new GetUsersForFinanceQuery());
            return Result(result);
        }
    }
}