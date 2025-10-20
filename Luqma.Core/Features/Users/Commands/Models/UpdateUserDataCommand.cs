using Luqma.Core.Bases;
using Luqma.Data.DTOs.Users;
using MediatR;

namespace Luqma.Core.Features.Users.Commands.Models
{
    public class UpdateUserDataCommand : IRequest<ApiResponse>
    {
        public UpdateUserDataDTO UserData { get; set; }
    }
}
