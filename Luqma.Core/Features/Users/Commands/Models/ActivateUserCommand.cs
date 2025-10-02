using Luqma.Core.Bases;
using MediatR;

namespace Luqma.Core.Features.Users.Commands.Models
{
    public class ActivateUserCommand : IRequest<ApiResponse>
    {
        public int UserId { get; set; }
    }
}
