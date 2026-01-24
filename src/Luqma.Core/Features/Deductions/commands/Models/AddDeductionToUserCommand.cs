using Luqma.Core.Bases;
using MediatR;

namespace Luqma.Core.Features.Deductions.Commands.Models
{
    public class AddDeductionToUserCommand : IRequest<ApiResponse>
    {
        public int UserId { get; set; }
        public double DeductionRate { get; set; }
    }
}
