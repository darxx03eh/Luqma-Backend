using Luqma.Core.Bases;
using Luqma.Data.DTOs.Users;
using MediatR;

namespace Luqma.Core.Features.Users.Commands.Models
{
    public class ChangeUserRolesCommand : IRequest<ApiResponse>
    {
        public int UserId { get; set; }
        public IList<UserRolesDTO> UserRoles { get; set; }
    }
}
