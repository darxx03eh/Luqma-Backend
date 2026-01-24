using Luqma.Core.Bases;
using MediatR;

namespace Luqma.Core.Features.Salaries.Commands.Models
{
    public class ChangeSalaryAmountCommand : IRequest<ApiResponse>
    {
        public int SalaryId { get; set; }
        public double SalaryAmount { get; set; }
    }
}
