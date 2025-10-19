using Luqma.Core.Bases;
using MediatR;

namespace Luqma.Core.Features.KitchenRequirments.Commands.Models
{
    public class DeleteKitchenRequirmentsCommand : IRequest<ApiResponse>
    {
        public int Id { get; set; }
        public DeleteKitchenRequirmentsCommand(int id) => Id = id;
    }
}
