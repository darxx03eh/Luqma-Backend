using Luqma.Core.Bases;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Luqma.Core.Features.Users.Commands.Models
{
    public class UploadProfileImageCommand : IRequest<ApiResponse>
    {
        public IFormFile ProfileImage { get; set; }
    }
}
