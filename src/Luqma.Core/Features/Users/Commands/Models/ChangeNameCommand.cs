using Luqma.Core.Bases;
using MediatR;

namespace Luqma.Core.Features.Users.Commands.Models
{
    public class ChangeNameCommand : IRequest<ApiResponse>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
