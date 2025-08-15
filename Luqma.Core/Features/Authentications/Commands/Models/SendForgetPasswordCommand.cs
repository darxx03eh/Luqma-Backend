using Luqma.Core.Bases;
using MediatR;

namespace Luqma.Core.Features.Authentications.Commands.Models
{
    public class SendForgetPasswordCommand : IRequest<ApiResponse>
    {
        public string Email { get; set; }
        public SendForgetPasswordCommand(string email)
            => Email = email;
    }
}
