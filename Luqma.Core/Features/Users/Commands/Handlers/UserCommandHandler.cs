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
        , IRequestHandler<UploadProfileImageCommand, ApiResponse>
        , IRequestHandler<ChangeUserNameCommand, ApiResponse>
        , IRequestHandler<DeleteProfileImageCommand, ApiResponse>
        , IRequestHandler<ChangeBirthDateCommand, ApiResponse>
        , IRequestHandler<ActivateUserCommand, ApiResponse>
        , IRequestHandler<DeactivateUserCommand, ApiResponse>
        , IRequestHandler<AddUserAddressCommand, ApiResponse>
        , IRequestHandler<UpdateUserAddressCommand, ApiResponse>
        , IRequestHandler<DeleteUserAddressCommand, ApiResponse>
        , IRequestHandler<ChangeUserRolesCommand, ApiResponse>
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
                "PasswordChangedSuccessfully" => Success(null, message: SharedResponseKeys.PasswordChangedSuccessfully),
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

        public async Task<ApiResponse> Handle(UploadProfileImageCommand request, CancellationToken cancellationToken)
        {
            var (result, imageUrl) = await userService.UploadProfileImageAsync(request.ProfileImage);
            return result switch
            {
                "UserNotFound" => NotFound(SharedResponseKeys.UserNotFound),
                "FailedToDeleteOldImageFromCloudinary" => InternalServerError(SharedResponseKeys.FailedToDeleteOldImageFromCloudinary),
                "AnErrorOccurredWhileEditingImage" => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileEditingImage),
                "TheImageHasBeenChangedSuccessfully" => Success(new
                {
                    imageUrl = imageUrl,
                }, message: SharedResponseKeys.TheImageHasBeenChangedSuccessfully),
                "AnErrorOccurredWhileProcessingYourProfileImageModificationRequest"
                => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileProcessingYourProfileImageModificationRequest),
                _ => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileProcessingYourProfileImageModificationRequest)
            };
        }

        public async Task<ApiResponse> Handle(ChangeUserNameCommand request, CancellationToken cancellationToken)
        {
            var (result, username) = await userService.ChangeUserNameAsync(request.UserName);
            return result switch
            {
                "UserNotFound" => NotFound(SharedResponseKeys.NotFound),
                "AnErrorOccurredWhileChangingTheUsername" => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileChangingTheUsername),
                "UsernameChangedSuccessfully" => Success(new
                {
                    userName = username,
                }, message: SharedResponseKeys.UsernameChangedSuccessfully),
                _ => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileChangingTheUsername)
            };
        }

        public async Task<ApiResponse> Handle(DeleteProfileImageCommand request, CancellationToken cancellationToken)
        {
            var result = await userService.DeleteProfileImageAsync();
            return result switch
            {
                "UserNotFound" => NotFound(SharedResponseKeys.UserNotFound),
                "ThereIsNoImageToDelete" => BadRequest(SharedResponseKeys.ThereIsNoImageToDelete),
                "FailedToDeleteImageFromCloudinary" => InternalServerError(SharedResponseKeys.FailedToDeleteImageFromCloudinary),
                "ImageHasBeenSuccessfullyDeleted" => Success(null, message: SharedResponseKeys.ImageHasBeenSuccessfullyDeleted),
                "AnErrorOccurredWhileSaving" => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileSaving),
                "AnErrorOccurredWhileDeletingTheImage" => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileDeletingTheImage),
                _ => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileDeletingTheImage)
            };
        }

        public async Task<ApiResponse> Handle(ChangeBirthDateCommand request, CancellationToken cancellationToken)
        {
            var result = await userService.ChangeBirthDateAsync(request.BirthDate);
            return result switch
            {
                "UserNotFound" => NotFound(SharedResponseKeys.NotFound),
                "AnErrorOccurredWhileChangingTheBirthDate" => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileChangingTheBirthDate),
                "BirthDateChangedSuccessfully" => Success(new
                {
                    birthDate = request.BirthDate,
                }, message: SharedResponseKeys.BirthDateChangedSuccessfully),
                _ => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileChangingTheBirthDate)
            };
        }
        public async Task<ApiResponse> Handle(ActivateUserCommand request, CancellationToken cancellationToken)
        {
            var result = await userService.ActivateAsync(request.UserId);
            return result switch
            {
                "UserNotFound" => NotFound(SharedResponseKeys.UserNotFound),
                "TheUserWhoseAccountYouWantToActivateIsNotFound" =>
                NotFound(SharedResponseKeys.TheUserWhoseAccountYouWantToActivateIsNotFound),
                "AnErrorOccurredWhileActivatingTheUser" =>
                InternalServerError(SharedResponseKeys.AnErrorOccurredWhileActivatingTheUser),
                "UserAlreadyActive" => BadRequest(SharedResponseKeys.UserAlreadyActive),
                "TheUserHasBeenActivatedSuccessfully" => Success(null, message: SharedResponseKeys.TheUserHasBeenActivatedSuccessfully),
                _ => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileActivatingTheUser)
            };
        }

        public async Task<ApiResponse> Handle(DeactivateUserCommand request, CancellationToken cancellationToken)
        {
            var result = await userService.DeActivateAsync(request.UserId);
            return result switch
            {
                "UserNotFound" => NotFound(SharedResponseKeys.UserNotFound),
                "TheUserNWhoseAccountYouWantToDeactivateIsNotFound" =>
                NotFound(SharedResponseKeys.TheUserWhoseAccountYouWantToDeactivateIsNotFound),
                "AnErrorOccurredWhileDeactivatingTheUser" =>
                InternalServerError(SharedResponseKeys.AnErrorOccurredWhileDeactivatingTheUser),
                "UserAlreadyInActive" => BadRequest(SharedResponseKeys.UserAlreadyInActive),
                "TheUserHasBeenDeactivatedSuccessfully" =>
                Success(null, message: SharedResponseKeys.TheUserHasBeenDeactivatedSuccessfully),
                _ => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileDeactivatingTheUser)
            };
        }

        public async Task<ApiResponse> Handle(AddUserAddressCommand request, CancellationToken cancellationToken)
        {
            var result = await userService.AddAddressAsync(request.City, request.State, request.Street);
            return result switch
            {
                "UserNotFound" => NotFound(SharedResponseKeys.UserNotFound),
                "AnErrorOccurredWhileAddingTheAddress" => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileAddingTheAddress),
                "TheAddressHasBeenAddedSuccessfully" => Success(new
                {
                    City = request.City,
                    State = request.State,
                    Street = request.Street,
                }, message: SharedResponseKeys.TheAddressHasBeenAddedSuccessfully),
                _ => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileAddingTheAddress)
            };
        }

        public async Task<ApiResponse> Handle(UpdateUserAddressCommand request, CancellationToken cancellationToken)
        {
            var result = await userService.UpdateAddressAsync(request.City, request.State, request.Street, request.AddressId);
            return result switch
            {
                "UserNotFound" => NotFound(SharedResponseKeys.UserNotFound),
                "AddressNotFound" => NotFound(SharedResponseKeys.AddressNotFound),
                "ThisAddressDoesNotBelongToYou" => BadRequest(SharedResponseKeys.ThisAddressDoesNotBelongToYou),
                "AnErrorOccurredWhileEditingTheAddress" => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileEditingTheAddress),
                "TheAddressHasBeenSuccessfullyModified" => Success(new
                {
                    AddressId = request.AddressId,
                    City = request.City,
                    State = request.State,
                    Street = request.Street,
                }, message: SharedResponseKeys.TheAddressHasBeenSuccessfullyModified),
                _ => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileEditingTheAddress)
            };
        }

        public async Task<ApiResponse> Handle(DeleteUserAddressCommand request, CancellationToken cancellationToken)
        {
            var result = await userService.DeleteAddressAsync(request.Id);
            return result switch
            {
                "UserNotFound" => NotFound(SharedResponseKeys.NotFound),
                "AddressNotFound" => NotFound(SharedResponseKeys.AddressNotFound),
                "ThisAddressDoesNotBelongToYou" => BadRequest(SharedResponseKeys.ThisAddressDoesNotBelongToYou),
                "AnErrorOccurredWhileDeletingTheAddress" => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileDeletingTheAddress),
                "TheAddressHasBeenSuccessfullyDeleted" => Success(null, message: SharedResponseKeys.TheAddressHasBeenSuccessfullyDeleted),
                _ => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileDeletingTheAddress)
            };
        }

        public async Task<ApiResponse> Handle(ChangeUserRolesCommand request, CancellationToken cancellationToken)
        {
            var result = await userService.ChangeUserRolesAsync(request.UserId, request.UserRoles);
            return result switch
            {
                "ManagerNotFound" => NotFound(SharedResponseKeys.ManagerNotFound),
                "UserNotFound" => NotFound(SharedResponseKeys.UserNotFound),
                "AnErrorOccurredWhileDeletingOldRoles" => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileDeletingOldRoles),
                "FailedToAddUserRoles" => InternalServerError(SharedResponseKeys.FailedToAddUserRoles),
                "AddedToUserRolesSuccessfully" => Success(null, message: SharedResponseKeys.AddedToUserRolesSuccessfully),
                "AnErrorOccurredWhileAddingTheUserToRoles" => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileAddingTheUserToRoles),
                _ => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileAddingTheUserToRoles)
            };
        }
    }
}
