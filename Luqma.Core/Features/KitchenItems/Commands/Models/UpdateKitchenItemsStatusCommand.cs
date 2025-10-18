using Luqma.Core.Bases;
using MediatR;

namespace Luqma.Core.Features.KitchenItems.Commands.Models
{
    public class UpdateKitchenItemsStatusCommand : IRequest<ApiResponse>
    {
        public int Id { get; set; }
        public string Status { get; set; }
    }
}
