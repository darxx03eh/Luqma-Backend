using Luqma.Core.Bases;
using MediatR;

namespace Luqma.Core.Features.Authentications.Commands.Models
{
    public class ForgetPasswordConfirmationCommand : IRequest<ApiResponse>
    {
        public string Email { get; set; }
        public string Code { get; set; }
    }
}
