using Luqma.Core.Bases;
using MediatR;

namespace Luqma.Core.Features.Users.Commands.Models
{
    public class ChangeBirthDateCommand : IRequest<ApiResponse>
    {
        public DateTime BirthDate { get; set; }
    }
}
