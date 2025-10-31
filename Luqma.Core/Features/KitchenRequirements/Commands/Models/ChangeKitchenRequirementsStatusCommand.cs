using Luqma.Core.Bases;
using MediatR;

namespace Luqma.Core.Features.KitchenRequirements.Commands.Models
{
    public class ChangeKitchenRequirementsStatusCommand : IRequest<ApiResponse>
    {
        public int Id { get; set; }
        public string Status { get; set; }
    }
}
