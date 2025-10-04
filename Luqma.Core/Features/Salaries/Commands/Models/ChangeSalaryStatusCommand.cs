using Luqma.Core.Bases;
using MediatR;

namespace Luqma.Core.Features.Salaries.Commands.Models
{
    public class ChangeSalaryStatusCommand : IRequest<ApiResponse>
    {
        public int SalaryId { get; set; }
        public string Status { get; set; }
    }
}
