using Luqma.Core.Bases;
using MediatR;

namespace Luqma.Core.Features.Users.Commands.Models
{
    public class ChangeSalaryCommand : IRequest<ApiResponse>
    {
        public int UserId { get; set; }
        public double Salary {  get; set; }
    }
}
