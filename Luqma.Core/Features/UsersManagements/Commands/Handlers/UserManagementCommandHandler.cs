using Luqma.Core.Bases;
using Luqma.Core.Features.UsersManagements.Commands.Models;
using Luqma.Core.ResponseKeys;
using Luqma.Service.Interfaces;
using MediatR;

namespace Luqma.Core.Features.UsersManagements.Commands.Handlers
{
    public class UserManagementCommandHandler : ApiResponseHandler
        , IRequestHandler<ActivateUserCommand, ApiResponse>
        , IRequestHandler<DeactivateUserCommand, ApiResponse>
    {
        private readonly IUsersManagementService usersManagementService;

        public UserManagementCommandHandler(IUsersManagementService usersManagementService)
        {
            this.usersManagementService = usersManagementService;
        }

        public async Task<ApiResponse> Handle(ActivateUserCommand request, CancellationToken cancellationToken)
        {
            var result = await usersManagementService.ActivateAsync(request.UserId);
            return result switch
            {
                "UserNotFound" => NotFound(SharedResponseKeys.UserNotFound),
                "TheUserWhoseAccountYouWantToActivateIsNotFound" =>
                NotFound(SharedResponseKeys.TheUserWhoseAccountYouWantToActivateIsNotFound),
                "AnErrorOccurredWhileActivatingTheUser" =>
                InternalServerError(SharedResponseKeys.AnErrorOccurredWhileActivatingTheUser),
                "TheUserHasBeenActivatedSuccessfully" => Success(null, message: SharedResponseKeys.TheUserHasBeenActivatedSuccessfully),
                _ => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileActivatingTheUser)
            };
        }

        public async Task<ApiResponse> Handle(DeactivateUserCommand request, CancellationToken cancellationToken)
        {
            var result = await usersManagementService.DeActivateAsync(request.UserId);
            return result switch
            {
                "UserNotFound" => NotFound(SharedResponseKeys.UserNotFound),
                "TheUserNWhoseAccountYouWantToDeactivateIsNotFound" =>
                NotFound(SharedResponseKeys.TheUserWhoseAccountYouWantToDeactivateIsNotFound),
                "AnErrorOccurredWhileDeactivatingTheUser" =>
                InternalServerError(SharedResponseKeys.AnErrorOccurredWhileDeactivatingTheUser),
                "TheUserHasBeenDeactivatedSuccessfully" =>
                Success(null, message: SharedResponseKeys.TheUserHasBeenDeactivatedSuccessfully),
                _ => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileDeactivatingTheUser)
            };
        }
    }
}
