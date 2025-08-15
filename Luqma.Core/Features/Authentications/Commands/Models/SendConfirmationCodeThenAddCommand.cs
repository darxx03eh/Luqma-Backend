using Luqma.Core.Bases;
using MediatR;

namespace Luqma.Core.Features.Authentications.Commands.Models
{
    public class SendConfirmationCodeThenAddCommand : IRequest<ApiResponse>
    {
        public string PhoneNumber { get; set; }
        public SendConfirmationCodeThenAddCommand(string phoneNumber)
            => PhoneNumber = phoneNumber;
    }
}
