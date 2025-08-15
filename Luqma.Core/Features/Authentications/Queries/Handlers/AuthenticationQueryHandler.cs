using Luqma.Core.Bases;
using Luqma.Core.Features.Authentications.Queries.Models;
using Luqma.Core.ResponseKeys;
using Luqma.Service.Interfaces;
using MediatR;

namespace Luqma.Core.Features.Authentications.Queries.Handlers
{
    public class AuthenticationQueryHandler : ApiResponseHandler
        , IRequestHandler<ValidateAccessTokenQuery, ApiResponse>
    {
        private readonly IAuthenticationService authenticationService;

        public AuthenticationQueryHandler(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        public async Task<ApiResponse> Handle(ValidateAccessTokenQuery request, CancellationToken cancellationToken)
        {
            var result = await authenticationService.ValidateAccessToken(request.AccessToken);
            return result switch
            {
                "InvalidTokenFormat" => BadRequest(SharedResponseKeys.InvalidTokenFormat),
                "TokenExpired" => BadRequest(SharedResponseKeys.TokenExpired),
                "AnErrorOccurredWhileVerifyingTheToken" =>
                InternalServerError(SharedResponseKeys.AnErrorOccurredWhileVerifyingTheToken),
                "ValidToken" => Success(null, message: SharedResponseKeys.ValidToken),
                _ => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileVerifyingTheToken)
            };
        }
    }
}
