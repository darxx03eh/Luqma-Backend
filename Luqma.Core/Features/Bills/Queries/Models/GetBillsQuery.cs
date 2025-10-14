using Luqma.Core.Bases;
using MediatR;

namespace Luqma.Core.Features.Bills.Queries.Models
{
    public class GetBillsQuery : IRequest<ApiResponse>
    {
        public int PageNumber { get; set; }
    }
}
