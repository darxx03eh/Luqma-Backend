using Luqma.Core.Bases;
using MediatR;

namespace Luqma.Core.Features.Users.Queries.Models
{
    public class GetUserProfileQuery : IRequest<ApiResponse>
    {
        public string UserName { get; set; }
        public GetUserProfileQuery(string username) => UserName = username;
    }
}
