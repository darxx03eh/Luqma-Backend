using Luqma.Core.Bases;
using MediatR;

namespace Luqma.Core.Features.Authentications.Commands.Models
{
    public class RevokeRefreshTokenCommand : IRequest<ApiResponse>
    {
        public string RefreshToken { get; set; }
    }
}
