using Luqma.Core.Bases;
using MediatR;

namespace Luqma.Core.Features.Users.Commands.Models
{
    public class AddUserAddressCommand : IRequest<ApiResponse>
    {
        public string City { get; set; }
        public string State { get; set; }
        public string Street { get; set; }
    }
}
