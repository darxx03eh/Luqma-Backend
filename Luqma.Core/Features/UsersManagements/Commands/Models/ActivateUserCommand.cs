using Luqma.Core.Bases;
using MediatR;

namespace Luqma.Core.Features.UsersManagements.Commands.Models
{
    public class ActivateUserCommand : IRequest<ApiResponse>
    {
        public int UserId { get; set; }
    }
}
