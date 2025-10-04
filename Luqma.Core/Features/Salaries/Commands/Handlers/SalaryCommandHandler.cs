using Luqma.Core.Bases;
using Luqma.Core.Features.Salaries.Commands.Models;
using Luqma.Core.ResponseKeys;
using Luqma.Service.Interfaces;
using MediatR;

namespace Luqma.Core.Features.Salaries.Commands.Handlers
{
    public class SalaryCommandHandler : ApiResponseHandler
        , IRequestHandler<GenerateSalariesCommand, ApiResponse>
        , IRequestHandler<DeleteSalaryCommand, ApiResponse>
        , IRequestHandler<ChangeSalaryStatusCommand, ApiResponse>
        , IRequestHandler<ChangeSalaryAmountCommand, ApiResponse>
    {
        private readonly ISalaryService salaryService;

        public SalaryCommandHandler(ISalaryService salaryService)
        {
            this.salaryService = salaryService;
        }
        public async Task<ApiResponse> Handle(GenerateSalariesCommand request, CancellationToken cancellationToken)
        {
            var result = await salaryService.GenerateSalaryAsync();
            return result switch
            {
                "FinanceEmployeeNotFound" => NotFound(SharedResponseKeys.FinanceEmployeeNotFound),
                "SalariesForThisYearAndMonthAlreadyGenerated" => Conflict(SharedResponseKeys.SalariesForThisYearAndMonthAlreadyGenerated),
                "SalariesGeneratedSuccessfully" => Success(null, message: SharedResponseKeys.SalariesGeneratedSuccessfully),
                "AnErrorOccurredWhileGeneratingSalaries" => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileGeneratingSalaries),
                _ => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileGeneratingSalaries),
            };
        }

        public async Task<ApiResponse> Handle(DeleteSalaryCommand request, CancellationToken cancellationToken)
        {
            var result = await salaryService.DeleteSalaryAsync(request.Id);
            return result switch
            {
                "FinanceEmployeeNotFound" => NotFound(SharedResponseKeys.FinanceEmployeeNotFound),
                "SalaryNotFound" => NotFound(SharedResponseKeys.SalaryNotFound),
                "AnErrorOccurredWhileDeletingTheSalary" => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileDeletingTheSalary),
                "SalaryDeletedSuccessfully" => Deleted(SharedResponseKeys.SalaryDeletedSuccessfully),
                _ => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileDeletingTheSalary)
            };
        }

        public async Task<ApiResponse> Handle(ChangeSalaryStatusCommand request, CancellationToken cancellationToken)
        {
            var result = await salaryService.ChangeSalaryStatusAsync(request.SalaryId, request.Status);
            return result switch
            {
                "FinanceEmployeeNotFound" => NotFound(SharedResponseKeys.FinanceEmployeeNotFound),
                "SalaryNotFound" => NotFound(SharedResponseKeys.SalaryNotFound),
                "AnErrorOccurredWhileUpdatingTheStatus" => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileUpdatingTheStatus),
                "StatusUpdatedSuccessfully" => Success(new
                {
                    SalaryId = request.SalaryId,
                    Status = request.Status,
                }, message: SharedResponseKeys.StatusUpdatedSuccessfully),
                _ => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileUpdatingTheStatus)
            };
        }

        public async Task<ApiResponse> Handle(ChangeSalaryAmountCommand request, CancellationToken cancellationToken)
        {
            var result = await salaryService.ChangeSalaryAmountAsync(request.SalaryId, request.SalaryAmount);
            return result switch
            {
                "FinanceEmployeeNotFound" => NotFound(SharedResponseKeys.FinanceEmployeeNotFound),
                "SalaryNotFound" => NotFound(SharedResponseKeys.SalaryNotFound),
                "AnErrorOccurredWhileUpdatingTheSalary" => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileUpdatingTheSalary),
                "AmountUpdatedSuccessfully" => Success(new
                {
                    SalaryId = request.SalaryId,
                    SalaryAmount = request.SalaryAmount,
                }, message: SharedResponseKeys.AmountUpdatedSuccessfully),
                _ => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileUpdatingTheSalary)
            };
        }
    }
}
