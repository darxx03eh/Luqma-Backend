using Luqma.Core.Bases;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Luqma.Core.Features.KitchenItems.Commands.Models
{
    public class AddKitchenItemsCommand : IRequest<ApiResponse>
    {
        public string Item { get; set; }
        public string Status { get; set; }
        public IFormFile? Image { get; set; }
        public string? Note { get; set; }
        public string Unit { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
    }
}
