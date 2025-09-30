using Luqma.Core.Bases;
using MediatR;

namespace Luqma.Core.Features.Users.Commands.Models
{
    public class ChangeUserNameCommand : IRequest<ApiResponse>
    {
        public string UserName { get; set; }
    }
}
