using Luqma.Core.Bases;
using MediatR;

namespace Luqma.Core.Features.Salaries.Commands.Models
{
    public class GenerateSalaryForUserCommand  : IRequest<ApiResponse>
    {
        public int Id { get; set; }
        public int? Year { get; set; }
        public int? Month { get; set; }
    }
}
