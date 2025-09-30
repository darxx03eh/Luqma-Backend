using Luqma.API.Base;
using Luqma.Core.Features.Authentications.Commands.Models;
using Luqma.Core.Features.Authentications.Queries.Models;
using Luqma.Data.Helpers;
using Luqma.Data.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Luqma.API.Controllers
{
    [ApiController]
    public class AuthenticationsController : AppBaseController
    {
        [Authorize(Roles = Roles.Manager)]
        [HttpPost(Router.AuthenticationsRouting.SignUp)]
        public async Task<IActionResult> RegistrationUser([FromBody] SignUpCommand request)
        {
            var result = await mediator.Send(request);
            return Result(result);
        }
        [HttpPost(Router.AuthenticationsRouting.SignIn)]
        public async Task<IActionResult> SignIn([FromBody] SignInCommand request)
        {
            var result = await mediator.Send(request);
            return Result(result);
        }
        [HttpGet(Router.AuthenticationsRouting.EmailConfirmation)]
        public async Task<IActionResult> EmailConfirmation([FromQuery] ConfirmationEmailCommand request)
        {
            var result = await mediator.Send(request);
            return Result(result);
        }
        [HttpPost(Router.AuthenticationsRouting.SendConfirmationEmail)]
        public async Task<IActionResult> SendConfirmationEmail([FromBody] SendConfirmationEmailCommand request)
        {
            var result = await mediator.Send(request);
            return Result(result);
        }
        [HttpPost(Router.AuthenticationsRouting.SendForgetPasswordEmail)]
        public async Task<IActionResult> SendForgetPasswordEmail([FromBody] SendForgetPasswordCommand request)
        {
            var result = await mediator.Send(request);
            return Result(result);
        }
        [HttpPost(Router.AuthenticationsRouting.ForgetPasswordConfirmation)]
        public async Task<IActionResult> ForgetPasswordConfirmation([FromBody] ForgetPasswordConfirmationCommand request)
        {
            var result = await mediator.Send(request);
            return Result(result);
        }
        [HttpPost(Router.AuthenticationsRouting.ResetPassword)]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand request)
        {
            var result = await mediator.Send(request);
            return Result(result);
        }
        [HttpPost(Router.AuthenticationsRouting.GenerateRefreshToken)]
        public async Task<IActionResult> GenerateRefreshToken([FromBody] GenerateRefreshTokenCommand request)
        {
            var result = await mediator.Send(request);
            return Result(result);
        }
        [HttpDelete(Router.AuthenticationsRouting.RevokeRefreshToken)]
        public async Task<IActionResult> RevokeRefreshToken([FromBody] RevokeRefreshTokenCommand request)
        {
            var result = await mediator.Send(request);
            return Result(result);
        }
        [HttpGet(Router.AuthenticationsRouting.ValidateAccessToken)]
        public async Task<IActionResult> ValidateAccessToken([FromQuery] String token)
        {
            var result = await mediator.Send(new ValidateAccessTokenQuery(token));
            return Result(result);
        }
        [Authorize]
        [HttpPost(Router.AuthenticationsRouting.SendConfirmationCodeThenAdd)]
        public async Task<IActionResult> SendConfirmationCodeThenAdd([FromBody] SendConfirmationCodeThenAddCommand request)
        {
            var result = await mediator.Send(request);
            return Result(result);
        }
        [Authorize]
        [HttpPost(Router.AuthenticationsRouting.PhoneNumberConfirmation)]
        public async Task<IActionResult> PhoneNumberConfirmation([FromBody] ConfirmationPhoneNumberCommand request)
        {
            var result = await mediator.Send(request);
            return Result(result);
        }
    }
}
