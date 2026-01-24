using Luqma.Core.Bases;
using MediatR;

namespace Luqma.Core.Features.Authentications.Commands.Models
{
    public class SendConfirmationEmailCommand : IRequest<ApiResponse>
    {
        public string UserName { get; set; }
        public SendConfirmationEmailCommand(string username)
            => UserName = username;
    }
}
