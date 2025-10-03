using Luqma.Core.Bases;
using MediatR;

namespace Luqma.Core.Features.Deductions.Queries.Models
{
    public class ShowDeductionsForSpecificYearAndMonthQuery : IRequest<ApiResponse>
    { 
        public int PageNumber { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public ShowDeductionsForSpecificYearAndMonthQuery(int year, int month)
        {
            Year = year;
            Month = month;
        }
    }
}
