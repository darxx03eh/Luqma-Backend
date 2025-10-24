using Luqma.Core.Bases;
using Luqma.Core.Features.Deductions.Queries.Models;
using Luqma.Core.ResponseKeys;
using Luqma.Service.Interfaces;
using MediatR;

namespace Luqma.Core.Features.Deductions.Queries.Handlers
{
    public class DeductionQueryHandler : ApiResponseHandler
        , IRequestHandler<ShowAllDeductionsQuery, ApiResponse>
        , IRequestHandler<ShowDeductionsForSpecificYearAndMonthQuery, ApiResponse>
        , IRequestHandler<ShowDeductionsForSpecificUserQuery, ApiResponse>
    {
        private readonly IDeductionService deductionService;

        public DeductionQueryHandler(IDeductionService deductionService)
        {
            this.deductionService = deductionService;
        }
        public async Task<ApiResponse> Handle(ShowAllDeductionsQuery request, CancellationToken cancellationToken)
        {
            var (result, deductions) = await deductionService.ShowAllDeductionsAsync(request.PageNumber);
            return result switch
            {
                "FinanceOrManagerNotFound" => NotFound(SharedResponseKeys.FinanceOrManagerNotFound),
                "DeductionsNotFound" => NotFound(SharedResponseKeys.DeductionsNotFound),
                "DeductionsFound" => Success(deductions, message: SharedResponseKeys.DeductionsFound),
                _ => NotFound(SharedResponseKeys.DeductionsNotFound)
            };
        }

        public async Task<ApiResponse> Handle(ShowDeductionsForSpecificYearAndMonthQuery request, CancellationToken cancellationToken)
        {
            var (result, deductions) = await deductionService.GetAllDeductionsByDateAsync(request.PageNumber, request.Year, request.Month);
            return result switch
            {
                "FinanceOrManagerNotFound" => NotFound(SharedResponseKeys.FinanceOrManagerNotFound),
                "DeductionsNotFound" => NotFound(SharedResponseKeys.DeductionsNotFound),
                "DeductionsFound" => Success(deductions, message: SharedResponseKeys.DeductionsFound),
                _ => NotFound(SharedResponseKeys.DeductionsNotFound)
            };
        }

        public async Task<ApiResponse> Handle(ShowDeductionsForSpecificUserQuery request, CancellationToken cancellationToken)
        {
            var (result, deductions) = await deductionService.GetAllDeductionsForSpecificUser(request.PageNumber, request.Name);
            return result switch
            {
                "FinanceOrManagerNotFound" => NotFound(SharedResponseKeys.FinanceOrManagerNotFound),
                "DeductionsNotFound" => NotFound(SharedResponseKeys.DeductionsNotFound),
                "DeductionsFound" => Success(deductions, message: SharedResponseKeys.DeductionsFound),
                _ => NotFound(SharedResponseKeys.DeductionsNotFound)
            };
        }
    }
}
