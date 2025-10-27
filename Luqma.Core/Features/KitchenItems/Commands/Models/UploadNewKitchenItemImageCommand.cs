using Luqma.Core.Bases;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Luqma.Core.Features.KitchenItems.Commands.Models
{
    public class UploadNewKitchenItemImageCommand : IRequest<ApiResponse>
    {
        public int Id { get; set; }
        public IFormFile Image { get; set; }
    }
}
