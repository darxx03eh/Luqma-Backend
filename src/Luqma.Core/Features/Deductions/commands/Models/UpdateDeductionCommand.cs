using Luqma.Core.Bases;
using MediatR;

namespace Luqma.Core.Features.Deductions.Commands.Models
{
    public class UpdateDeductionCommand : IRequest<ApiResponse>
    {
        public int DeductionId { get; set; }
        public double DeductionRate { get; set; }
    }
}
