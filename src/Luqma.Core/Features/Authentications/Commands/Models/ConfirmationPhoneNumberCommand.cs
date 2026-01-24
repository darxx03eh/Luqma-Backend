using Luqma.Core.Bases;
using MediatR;

namespace Luqma.Core.Features.Authentications.Commands.Models
{
    public class ConfirmationPhoneNumberCommand : IRequest<ApiResponse>
    {
        public string Code { get; set; }
        public ConfirmationPhoneNumberCommand(string code)
            => Code = code;
    }
}
