using Luqma.Core.Bases;
using MediatR;

namespace Luqma.Core.Features.Deductions.Queries.Models
{
    public class ShowDeductionsForSpecificUserQuery : IRequest<ApiResponse>
    {
        public int PageNumber { get; set; }
        public string Name { get; set; }
        public ShowDeductionsForSpecificUserQuery(string name) => Name = name;
    }
}
