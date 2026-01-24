using Luqma.Core.Bases;
using MediatR;

namespace Luqma.Core.Features.Users.Queries.Models
{
    public class ViewSpecificAddressQuery : IRequest<ApiResponse>
    {
        public int Id { get; set; }
        public ViewSpecificAddressQuery(int id) => Id = id;
    }
}
