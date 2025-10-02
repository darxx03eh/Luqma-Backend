using Luqma.Core.Bases;
using MediatR;

namespace Luqma.Core.Features.Deductions.commands.Models
{
    public class RemoveDeductionFromUserCommand : IRequest<ApiResponse>
    {
        public int Id { get; set; }
        public RemoveDeductionFromUserCommand(int id) => Id = id;
    }
}
