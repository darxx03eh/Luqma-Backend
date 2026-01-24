using Luqma.Core.Bases;
using MediatR;

namespace Luqma.Core.Features.KitchenRequirements.Commands.Models
{
    public class DeletePendingRequirementsCommand : IRequest<ApiResponse>
    {
        public int Id { get; set; }
        public DeletePendingRequirementsCommand(int id) => Id = id;
    }
}
