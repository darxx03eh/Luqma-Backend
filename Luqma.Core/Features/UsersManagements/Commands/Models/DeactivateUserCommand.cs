using Luqma.Core.Bases;
using MediatR;

namespace Luqma.Core.Features.UsersManagements.Commands.Models
{
    public class DeactivateUserCommand : IRequest<ApiResponse>
    {
        public int UserId { get; set; }
    }
}
