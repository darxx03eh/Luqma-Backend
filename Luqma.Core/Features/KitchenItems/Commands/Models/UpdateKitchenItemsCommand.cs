using Luqma.Core.Bases;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Luqma.Core.Features.KitchenItems.Commands.Models
{
    public class UpdateKitchenItemsCommand : IRequest<ApiResponse>
    {
        public int Id { get; set; }
        public string Item { get; set; }
        public string Status { get; set; }
        public string? Note { get; set; }
        public string Unit { get; set; }
        public double Quantity { get; set; }
    }
}
