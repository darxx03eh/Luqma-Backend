using Luqma.Core.Bases;
using MediatR;

namespace Luqma.Core.Features.Salaries.Queries.Models
{
    public class GetSalariesQuery : IRequest<ApiResponse>
    {
        public int PageNumber { get; set; }
        public string? Status {  get; set; }
        public string? Name { get; set; }
        public int? Year { get; set; }
        public int? Month { get; set; }
    }
}
