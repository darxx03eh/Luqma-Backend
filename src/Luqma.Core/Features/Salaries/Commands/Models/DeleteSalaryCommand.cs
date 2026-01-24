using Luqma.Core.Bases;
using MediatR;

namespace Luqma.Core.Features.Salaries.Commands.Models
{
    public class DeleteSalaryCommand : IRequest<ApiResponse>
    {
        public int Id { get; set; }
        public DeleteSalaryCommand(int id) => Id = id;
    }
}
