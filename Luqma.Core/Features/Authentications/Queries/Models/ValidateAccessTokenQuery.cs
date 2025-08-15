using Luqma.Core.Bases;
using MediatR;

namespace Luqma.Core.Features.Authentications.Queries.Models
{
    public class ValidateAccessTokenQuery : IRequest<ApiResponse>
    {
        public String AccessToken { get; set; }
        public ValidateAccessTokenQuery(String token)
            => AccessToken = token;
    }
}
