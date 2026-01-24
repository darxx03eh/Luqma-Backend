using AutoMapper;
using Luqma.Core.Bases;
using Luqma.Core.Features.Authentications.Commands.Models;
using Luqma.Core.ResponseKeys;
using Luqma.Data.Entities.Identity;
using Luqma.Service.Interfaces;
using MediatR;

namespace Luqma.Core.Features.Authentications.Commands.Handlers
{
    public class AuthenticationCommandHandler : ApiResponseHandler
        , IRequestHandler<SignUpCommand, ApiResponse>
        , IRequestHandler<SignInCommand, ApiResponse>
        , IRequestHandler<ConfirmationEmailCommand, ApiResponse>
        , IRequestHandler<SendConfirmationEmailCommand, ApiResponse>
        , IRequestHandler<SendForgetPasswordCommand, ApiResponse>
        , IRequestHandler<ForgetPasswordConfirmationCommand, ApiResponse>
        , IRequestHandler<ResetPasswordCommand, ApiResponse>
        , IRequestHandler<GenerateRefreshTokenCommand, ApiResponse>
        , IRequestHandler<RevokeRefreshTokenCommand, ApiResponse>
        , IRequestHandler<SendConfirmationCodeThenAddCommand, ApiResponse>
        , IRequestHandler<ConfirmationPhoneNumberCommand, ApiResponse>
    {
        private readonly IAuthenticationService authenticationService;
        private readonly IMapper mapper;

        public AuthenticationCommandHandler(IAuthenticationService authenticationService, IMapper mapper)
        {
            this.authenticationService = authenticationService;
            this.mapper = mapper;
        }

        public async Task<ApiResponse> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            var user = mapper.Map<LuqmaUser>(request);
            var result = await authenticationService.SignUpAsync(user, request.Password, request.Role);
            return result switch
            {
                "ErrorDuringAccountCreationProcess" => InternalServerError(SharedResponseKeys.ErrorDuringAccountCreationProcess),
                "RoleNotExist" => NotFound(SharedResponseKeys.RoleNotExist),
                "ErrorWhileAddingRole" => InternalServerError(SharedResponseKeys.ErrorWhileAddingRole),
                "AnErrorOccurredWhileSendingTheConfirmationEmailPleaseTryAgain"
                => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileSendingTheConfirmationEmailPleaseTryAgain),
                "AnErrorOccurredDuringTheRegistrationProcess"
                => InternalServerError(SharedResponseKeys.AnErrorOccurredDuringTheRegistrationProcess),
                "TheAccountHasBeenCreated" => Created(null, message: SharedResponseKeys.TheAccountHasBeenCreated),
                _ => InternalServerError(SharedResponseKeys.AnErrorOccurredDuringTheRegistrationProcess),
            };
        }

        public async Task<ApiResponse> Handle(ConfirmationEmailCommand request, CancellationToken cancellationToken)
        {
            var result = await authenticationService.ConfirmationEmailAsync(request.Email, request.Token);
            return result switch
            {
                "UserNotFound" => NotFound(SharedResponseKeys.UserNotFound),
                "InvalidOrExpiredToken" => Unauthorized(SharedResponseKeys.InvalidOrExpiredToken),
                "AnErrorOccurredDuringTheEmailConfirmationProcess" =>
                InternalServerError(SharedResponseKeys.AnErrorOccurredDuringTheEmailConfirmationProcess),
                "EmailConfirmedSuccessfully" => Success(null, message: SharedResponseKeys.EmailConfirmedSuccessfully),
                _ => InternalServerError(SharedResponseKeys.AnErrorOccurredDuringTheEmailConfirmationProcess),
            };
        }

        public async Task<ApiResponse> Handle(SendConfirmationEmailCommand request, CancellationToken cancellationToken)
        {
            var result = await authenticationService.SendConfirmationEmailAsync(request.UserName);
            return result switch
            {
                "UserNotFound" => NotFound(SharedResponseKeys.UserNotFound),
                "AnErrorOccurredWhileSendingTheConfirmationEmailPleaseTryAgain" =>
                InternalServerError(SharedResponseKeys.AnErrorOccurredWhileSendingTheConfirmationEmailPleaseTryAgain),
                "YourAccountHasAlreadyBeenConfirmed" => BadRequest(SharedResponseKeys.YourAccountHasAlreadyBeenConfirmed),
                "EmailConfirmationEmailHasBeenSent" => Success(null, message: SharedResponseKeys.EmailConfirmationEmailHasBeenSent),
                _ => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileSendingTheConfirmationEmailPleaseTryAgain)
            };
        }

        public async Task<ApiResponse> Handle(SendForgetPasswordCommand request, CancellationToken cancellationToken)
        {
            var result = await authenticationService.SendForgetPasswordAsync(request.Email);
            return result switch
            {
                "UserNotFound" => NotFound(SharedResponseKeys.UserNotFound),
                "AnErrorOccurredWhileSavingTheCode" => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileSavingTheCode),
                "AnErrorOccurredWhileSendingTheForgetPasswordEmailPleaseTryAgain" =>
                InternalServerError(SharedResponseKeys.AnErrorOccurredWhileSendingTheForgetPasswordEmailPleaseTryAgain),
                "ForgetPasswordEmailHasBeenSent" => Success(null, message: SharedResponseKeys.ForgetPasswordEmailHasBeenSent),
                _ => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileSendingTheForgetPasswordEmailPleaseTryAgain)
            };
        }

        public async Task<ApiResponse> Handle(ForgetPasswordConfirmationCommand request, CancellationToken cancellationToken)
        {
            var (result, token) = await authenticationService.ForgetPasswordConfirmationAsync(request.Email, request.Code);
            return result switch
            {
                "UserNotFound" => NotFound(SharedResponseKeys.UserNotFound),
                "TheCodeHasExpired" => BadRequest(SharedResponseKeys.TheCodeHasExpired),
                "TheCodeEnteredIsIncorrect" => BadRequest(SharedResponseKeys.TheCodeEnteredIsIncorrect),
                "AnErrorOccurredWhileDeletingTheCode" => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileDeletingTheCode),
                "AnErrorOccurredWhileSavingThePasswordresetToken" =>
                InternalServerError(SharedResponseKeys.AnErrorOccurredWhileSavingThePasswordresetToken),
                "TheCodeHasbeenVerified" => Success(new
                {
                    Email = request.Email,
                    Token = token
                }, message: SharedResponseKeys.TheCodeHasbeenVerified),
                _ => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileSavingThePasswordresetToken)
            };
        }

        public async Task<ApiResponse> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var result = await authenticationService.ResetPasswordAsync(request.Email, request.Password, request.Token);
            return result switch
            {
                "UserNotFound" => NotFound(SharedResponseKeys.UserNotFound),
                "AnErrorOccurredWhileDeletingTheOldPassword" =>
                InternalServerError(SharedResponseKeys.AnErrorOccurredWhileDeletingTheOldPassword),
                "AnErrorOccurredWhileAddingTheNewPassword" =>
                InternalServerError(SharedResponseKeys.AnErrorOccurredWhileAddingTheNewPassword),
                "AnErrorOccurredWhileChangingThePassword" =>
                InternalServerError(SharedResponseKeys.AnErrorOccurredWhileChangingThePassword),
                "InvalidPasswordResetToken" => BadRequest(SharedResponseKeys.InvalidPasswordResetToken),
                "ThereWasAProblemDeletingThePasswordResetToken" =>
                InternalServerError(SharedResponseKeys.ThereWasAProblemDeletingThePasswordResetToken),
                "PasswordChangedSuccessfully" => Success(null, message: SharedResponseKeys.PasswordChangedSuccessfully),
                _ => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileChangingThePassword)
            };
        }

        public async Task<ApiResponse> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            var (result, message) = await authenticationService.SignInAsync(request.UserName, request.Password);
            return message switch
            {
                "EmailNotConfirmed" => Forbidden(SharedResponseKeys.EmailNotConfirmed),
                "PasswordOrUserNameWrnog" => Unauthorized(SharedResponseKeys.PasswordOrUserNameWrnog),
                "AnErrorOccurredWhileGeneratingTheToken" =>
                InternalServerError(SharedResponseKeys.AnErrorOccurredWhileGeneratingTheToken),
                "AnErrorOccurredDuringTheLoginProcess" =>
                InternalServerError(SharedResponseKeys.AnErrorOccurredDuringTheLoginProcess),
                "YourAccountIsInactivePleaseCheckWithTechnicalSupport" =>
                Forbidden(SharedResponseKeys.YourAccountIsInactivePleaseCheckWithTechnicalSupport),
                "YourAccountWasLockedDueToSuspiciousActivityPleaseTryAgainLaterorContactSupport" =>
                Locked(SharedResponseKeys.YourAccountWasLockedDueToSuspiciousActivityPleaseTryAgainLaterorContactSupport),
                "DataVerifiedAndLogin" => Success(result, message: SharedResponseKeys.DataVerifiedAndLogin),
                _ => InternalServerError(SharedResponseKeys.AnErrorOccurredDuringTheLoginProcess)
            };
        }

        public async Task<ApiResponse> Handle(RevokeRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var result = await authenticationService.RevokeRefreshTokenAsync(request.RefreshToken);
            return result switch
            {
                "InvalidRefreshToken" => BadRequest(SharedResponseKeys.InvalidRefreshToken),
                "AnErrorOccurredDuringTheRefreshTokenCancellationProcess" =>
                InternalServerError(SharedResponseKeys.AnErrorOccurredDuringTheRefreshTokenCancellationProcess),
                "TokenRevokedSuccessfully" => Success(null, message: SharedResponseKeys.TokenRevokedSuccessfully),
                _ => InternalServerError(SharedResponseKeys.AnErrorOccurredDuringTheRefreshTokenCancellationProcess)
            };
        }

        public async Task<ApiResponse> Handle(GenerateRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var (response, result) = await authenticationService.GenerateRefreshTokenAsync(request.AccessToken, request.RefreshToken);
            return result switch
            {
                "ErrorInTheEncryptionAlgorithmUsed" =>
                InternalServerError(SharedResponseKeys.ErrorInTheEncryptionAlgorithmUsed),
                "TokenIsStillValidCannotRefreshYet" => BadRequest(SharedResponseKeys.TokenIsStillValidCannotRefreshYet),
                "RefreshTokenIsNotFound" => NotFound(SharedResponseKeys.RefreshTokenIsNotFound),
                "RefreshTokenHasExpire" => Unauthorized(SharedResponseKeys.RefreshTokenHasExpire),
                "UserNotFound" => NotFound(SharedResponseKeys.UserNotFound),
                "AnErrorOccurredDuringTheTokenGenerationProcess" =>
                InternalServerError(SharedResponseKeys.AnErrorOccurredDuringTheTokenGenerationProcess),
                "AccessTokenRegenerated" => Success(response, message: SharedResponseKeys.AccessTokenRegenerated), 
                _ => InternalServerError(SharedResponseKeys.AnErrorOccurredDuringTheTokenGenerationProcess)
            };
        }

        public async Task<ApiResponse> Handle(SendConfirmationCodeThenAddCommand request, CancellationToken cancellationToken)
        {
            var result = await authenticationService.SendConfirmationCodeThenAddAsync(request.PhoneNumber);
            return result switch
            {
                "YouMustLoginFirst" => Unauthorized(SharedResponseKeys.YouMustLoginFirst),
                "UserNotFound" => NotFound(SharedResponseKeys.UserNotFound),
                "AnErrorOccurredWhileSavingTheCode" => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileSavingTheCode),
                "AnErrorOccurredWhileAddingYourPhoneNumber" => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileAddingYourPhoneNumber),
                "AnErrorOccurredWhileSendingTheNumberConfirmationCodeToYourPhone" =>
                InternalServerError(SharedResponseKeys.AnErrorOccurredWhileSendingTheNumberConfirmationCodeToYourPhone),
                "PhoneNumberConfirmationCodeHasBeenSent" => Success(null, message: SharedResponseKeys.PhoneNumberConfirmationCodeHasBeenSent),
                "YourPhoneNumberAlreadyConfirmed" => Conflict(SharedResponseKeys.YourPhoneNumberAlreadyConfirmed),
                _ => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileSendingTheNumberConfirmationCodeToYourPhone)
            };
        }

        public async Task<ApiResponse> Handle(ConfirmationPhoneNumberCommand request, CancellationToken cancellationToken)
        {
            var result = await authenticationService.ConfirmationPhoneNumberAsync(request.Code);
            return result switch
            {
                "YouMustLoginFirst" => Unauthorized(SharedResponseKeys.YouMustLoginFirst),
                "UserNotFound" => NotFound(SharedResponseKeys.UserNotFound),
                "YourPhoneNumberAlreadyConfirmed" => Conflict(SharedResponseKeys.YourPhoneNumberAlreadyConfirmed),
                "TheCodeHasExpired" => BadRequest(SharedResponseKeys.TheCodeHasExpired),
                "TheCodeEnteredIsIncorrect" => BadRequest(SharedResponseKeys.TheCodeEnteredIsIncorrect),
                "AnErrorOccurredWhileDeletingTheCode" => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileDeletingTheCode),
                "YourPhoneNumberHasConfirmed" => Success(null, message:SharedResponseKeys.YourPhoneNumberHasConfirmed),
                "ThereWasAnErrorConfirmingYourPhoneNumber" => InternalServerError(SharedResponseKeys.ThereWasAnErrorConfirmingYourPhoneNumber),
                _ => InternalServerError(SharedResponseKeys.ThereWasAnErrorConfirmingYourPhoneNumber)
            };
        }
    }
}
