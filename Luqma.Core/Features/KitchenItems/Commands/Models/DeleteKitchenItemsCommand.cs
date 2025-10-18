using Luqma.Core.Bases;
using MediatR;

namespace Luqma.Core.Features.KitchenItems.Commands.Models
{
    public class DeleteKitchenItemsCommand : IRequest<ApiResponse>
    {
        public int Id { get; set; }
        public DeleteKitchenItemsCommand(int id) => Id = id;
    }
}
