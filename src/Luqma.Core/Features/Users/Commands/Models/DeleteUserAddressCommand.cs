using Luqma.Core.Bases;
using MediatR;

namespace Luqma.Core.Features.Users.Commands.Models
{
    public class DeleteUserAddressCommand : IRequest<ApiResponse>
    {
        public int Id { get; set; }
        public DeleteUserAddressCommand(int id) => Id = id;
    }
}
