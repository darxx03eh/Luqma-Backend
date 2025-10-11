using Luqma.Core.Bases;
using MediatR;

namespace Luqma.Core.Features.Bills.Commands.Models
{
    public class UpdateBillStatusCommand : IRequest<ApiResponse>
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public UpdateBillStatusCommand(int id, string status) => (Id, Status) = (id, status);
    }
}
