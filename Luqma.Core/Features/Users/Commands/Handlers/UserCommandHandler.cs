using Luqma.Core.Bases;
using Luqma.Core.Features.Users.Commands.Models;
using Luqma.Core.ResponseKeys;
using Luqma.Service.Interfaces;
using MediatR;

namespace Luqma.Core.Features.Users.Commands.Handlers
{
    public class UserCommandHandler : ApiResponseHandler
        , IRequestHandler<ChangePasswordCommand, ApiResponse>
    {
        private readonly IUserService userService;

        public UserCommandHandler(IUserService userService)
        {
            this.userService = userService;
        }
        public async Task<ApiResponse> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var result = await userService.ChangePasswordAsync(request.CurrentPassword, request.NewPassword);
            return result switch
            {
                "UserNotFound" => NotFound(SharedResponseKeys.UserNotFound),
                "CurrentPasswordWrong" => Unauthorized(SharedResponseKeys.CurrentPasswordWrong),
                "AnErrorOccurredWhileDeletingTheOldPassword" => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileDeletingTheOldPassword),
                "AnErrorOccurredWhileAddingTheNewPassword" => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileAddingTheNewPassword),
                "PasswordChangedSuccessfully" => Success(SharedResponseKeys.PasswordChangedSuccessfully),
                "AnErrorOccurredWhileChangingThePassword" => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileChangingThePassword),
                _ => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileChangingThePassword)
            };
        }
    }
}
