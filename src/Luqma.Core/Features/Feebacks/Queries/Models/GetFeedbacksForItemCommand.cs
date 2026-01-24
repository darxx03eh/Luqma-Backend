using Luqma.Core.Bases;
using MediatR;

namespace Luqma.Core.Features.Feebacks.Queries.Models
{
    public class GetFeedbacksForItemCommand : IRequest<ApiResponse>
    {
        public int Id { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
