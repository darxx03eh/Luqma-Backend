using Luqma.Core.Bases;
using MediatR;

namespace Luqma.Core.Features.Users.Queries.Models
{
    public class ViewUsersQuery : IRequest<ApiResponse>
    {
        public int PageNumber { get; set; }
    }
}
