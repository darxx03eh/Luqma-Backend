using Luqma.API.Base;
using Luqma.Core.Features.Authentications.Commands.Models;
using Luqma.Core.Features.Authentications.Queries.Models;
using Luqma.Data.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Luqma.API.Controllers
{
    [ApiController]
    public class AuthenticationsController : AppBaseController
    {
        [Authorize(Roles = "Manager")]
        [HttpPost(Router.AuthenticationRouting.SignUp)]
        public async Task<IActionResult> RegistrationUser([FromBody] SignUpCommand request)
        {
            var result = await mediator.Send(request);
            return Result(result);
        }
        [HttpPost(Router.AuthenticationRouting.SignIn)]
        public async Task<IActionResult> SignIn([FromForm] SignInCommand request)
        {
            var result = await mediator.Send(request);
            return Result(result);
        }
        [HttpGet(Router.AuthenticationRouting.EmailConfirmation)]
        public async Task<IActionResult> EmailConfirmation([FromQuery] ConfirmationEmailCommand request)
        {
            var result = await mediator.Send(request);
            return Result(result);
        }
        [HttpPost(Router.AuthenticationRouting.SendConfirmationEmail)]
        public async Task<IActionResult> SendConfirmationEmail([FromForm] String email)
        {
            var result = await mediator.Send(new SendConfirmationEmailCommand(email));
            return Result(result);
        }
        [HttpPost(Router.AuthenticationRouting.SendForgetPasswordEmail)]
        public async Task<IActionResult> SendForgetPasswordEmail([FromForm] String email)
        {
            var result = await mediator.Send(new SendForgetPasswordCommand(email));
            return Result(result);
        }
        [HttpPost(Router.AuthenticationRouting.ForgetPasswordConfirmation)]
        public async Task<IActionResult> ForgetPasswordConfirmation([FromForm] ForgetPasswordConfirmationCommand request)
        {
            var result = await mediator.Send(request);
            return Result(result);
        }
        [HttpPost(Router.AuthenticationRouting.ResetPassword)]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand request)
        {
            var result = await mediator.Send(request);
            return Result(result);
        }
        [HttpPost(Router.AuthenticationRouting.GenerateRefreshToken)]
        public async Task<IActionResult> GenerateRefreshToken([FromBody] GenerateRefreshTokenCommand request)
        {
            var result = await mediator.Send(request);
            return Result(result);
        }
        [HttpPost(Router.AuthenticationRouting.RevokeRefreshToken)]
        public async Task<IActionResult> RevokeRefreshToken([FromBody] RevokeRefreshTokenCommand request)
        {
            var result = await mediator.Send(request);
            return Result(result);
        }
        [HttpGet(Router.AuthenticationRouting.ValidateAccessToken)]
        public async Task<IActionResult> ValidateAccessToken([FromQuery] String token)
        {
            var result = await mediator.Send(new ValidateAccessTokenQuery(token));
            return Result(result);
        }
        [Authorize]
        [HttpPost(Router.AuthenticationRouting.SendConfirmationCodeThenAdd)]
        public async Task<IActionResult> SendConfirmationCodeThenAdd([FromForm] string phoneNumber)
        {
            var result = await mediator.Send(new SendConfirmationCodeThenAddCommand(phoneNumber));
            return Result(result);
        }
        [Authorize]
        [HttpPost(Router.AuthenticationRouting.PhoneNumberConfirmation)]
        public async Task<IActionResult> PhoneNumberConfirmation([FromForm] string code)
        {
            var result = await mediator.Send(new ConfirmationPhoneNumberCommand(code));
            return Result(result);
        }
    }
}
