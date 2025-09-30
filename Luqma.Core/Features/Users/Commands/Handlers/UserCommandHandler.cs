using Luqma.Core.Bases;
using Luqma.Core.Features.Users.Commands.Models;
using Luqma.Core.ResponseKeys;
using Luqma.Service.Interfaces;
using MediatR;

namespace Luqma.Core.Features.Users.Commands.Handlers
{
    public class UserCommandHandler : ApiResponseHandler
        , IRequestHandler<ChangePasswordCommand, ApiResponse>
        , IRequestHandler<ChangeNameCommand, ApiResponse>
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
                "PasswordChangedSuccessfully" => Success(null, message:SharedResponseKeys.PasswordChangedSuccessfully),
                "AnErrorOccurredWhileChangingThePassword" => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileChangingThePassword),
                _ => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileChangingThePassword)
            };
        }

        public async Task<ApiResponse> Handle(ChangeNameCommand request, CancellationToken cancellationToken)
        {
            var result = await userService.ChangeNameAsync(request.FirstName, request.LastName);
            return result switch
            {
                "UserNotFound" => NotFound(SharedResponseKeys.UserNotFound),
                "AnErrorOccurredWhileChangingTheFirstName" => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileChangingTheFirstName),
                "AnErrorOccurredWhileChangingTheLastName" => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileChangingTheLastName),
                "NameChangedSuccessfully" => Success(new
                {
                    firstName = request.FirstName,
                    lastName = request.LastName,
                }, message: SharedResponseKeys.NameChangedSuccessfully),
                "AnErrorOccurredWhileChangingTheName" => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileChangingTheName),
                _ => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileChangingTheName)
            };
        }
    }
}
