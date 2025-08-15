using Luqma.Core.Bases;
using MediatR;

namespace Luqma.Core.Features.Authentications.Commands.Models
{
    public class SignInCommand : IRequest<ApiResponse>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
