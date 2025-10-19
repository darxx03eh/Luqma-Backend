using Luqma.Core.Bases;
using MediatR;

namespace Luqma.Core.Features.KitchenRequirments.Commands.Models
{
    public class ChangeKitchenRequirmentsStatusCommand : IRequest<ApiResponse>
    {
        public int Id { get; set; }
        public string Status { get; set; }
    }
}
