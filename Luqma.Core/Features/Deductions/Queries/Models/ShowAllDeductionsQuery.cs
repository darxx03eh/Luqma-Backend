using Luqma.Core.Bases;
using MediatR;

namespace Luqma.Core.Features.Deductions.Queries.Models
{
    public class ShowAllDeductionsQuery : IRequest<ApiResponse>
    {
        public int PageNumber { get; set; }
    }
}
