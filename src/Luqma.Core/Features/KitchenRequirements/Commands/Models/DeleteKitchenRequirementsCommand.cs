using Luqma.Core.Bases;
using MediatR;

namespace Luqma.Core.Features.KitchenRequirements.Commands.Models
{
    public class DeleteKitchenRequirementsCommand : IRequest<ApiResponse>
    {
        public int Id { get; set; }
        public DeleteKitchenRequirementsCommand(int id) => Id = id;
    }
}
