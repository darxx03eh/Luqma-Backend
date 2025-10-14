using Luqma.Core.Bases;
using MediatR;

namespace Luqma.Core.Features.Bills.Commands.Models
{
    public class DeleteBillCommand : IRequest<ApiResponse>
    {
        public int Id { get; set; }
        public DeleteBillCommand(int id) => Id = id;
    }
}
