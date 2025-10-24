using Luqma.Core.Bases;
using Luqma.Core.Features.Salaries.Queries.Models;
using Luqma.Core.ResponseKeys;
using Luqma.Service.Interfaces;
using MediatR;

namespace Luqma.Core.Features.Salaries.Queries.Handlers
{
    public class SalaryQueryHandler : ApiResponseHandler
        , IRequestHandler<GetSalariesQuery, ApiResponse>
    {
        private readonly ISalaryService salaryService;

        public SalaryQueryHandler(ISalaryService salaryService)
        {
            this.salaryService = salaryService;
        }

        public async Task<ApiResponse> Handle(GetSalariesQuery request, CancellationToken cancellationToken)
        {
            var (result, salaries) = await salaryService.GetSalariesAsync(request.PageNumber, request.Name, request.Status,
                                                                         request.Year, request.Month);
            return result switch
            {
                "FinanceOrManagerNotFound" => NotFound(SharedResponseKeys.FinanceOrManagerNotFound),
                "SalariesNotFound" => NotFound(SharedResponseKeys.SalariesNotFound),
                "SalariesFound" => Success(salaries, message: SharedResponseKeys.SalariesFound),
                _ => NotFound(SharedResponseKeys.SalariesNotFound)
            };
        }
    }
}
