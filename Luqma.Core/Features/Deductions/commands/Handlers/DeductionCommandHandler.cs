using Luqma.Core.Bases;
using Luqma.Core.Features.Deductions.Commands.Models;
using Luqma.Core.ResponseKeys;
using Luqma.Service.Interfaces;
using MediatR;

namespace Luqma.Core.Features.Deductions.Commands.Handlers
{
    public class DeductionCommandHandler : ApiResponseHandler
        , IRequestHandler<AddDeductionToUserCommand, ApiResponse>
        , IRequestHandler<RemoveDeductionFromUserCommand, ApiResponse>
        , IRequestHandler<UpdateDeductionCommand, ApiResponse>
    {
        private readonly IDeductionService deductionService;

        public DeductionCommandHandler(IDeductionService deductionService)
        {
            this.deductionService = deductionService;
        }
        public async Task<ApiResponse> Handle(AddDeductionToUserCommand request, CancellationToken cancellationToken)
        {
            var result = await deductionService.AddDeductionToUserAsync(request.UserId, request.DeductionRate);
            return result switch
            {
                "UserNotFound" => NotFound(SharedResponseKeys.UserNotFound),
                "FinanceEmployeeNotFound" => NotFound(SharedResponseKeys.FinanceEmployeeNotFound),
                "AnErrorOccurredWhileAddingTheDeduction" => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileAddingTheDeduction),
                "TheDeductionHasBeenAddedSuccessfully" => Success(null, message: SharedResponseKeys.TheDeductionHasBeenAddedSuccessfully),
                _ => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileAddingTheDeduction)
            };
        }

        public async Task<ApiResponse> Handle(RemoveDeductionFromUserCommand request, CancellationToken cancellationToken)
        {
            var result = await deductionService.RemoveDeductionFromUserAsync(request.Id);
            return result switch
            {
                "FinanceEmployeeNotFound" => NotFound(SharedResponseKeys.FinanceEmployeeNotFound),
                "DeductionNotFound" => NotFound(SharedResponseKeys.DeductionNotFound),
                "AnErrorOccurredWhileDeletingTheDeduction" =>
                InternalServerError(SharedResponseKeys.AnErrorOccurredWhileDeletingTheDeduction),
                "TheDeductionHasBeenSuccessfullyRemoved" => Success(null, message: SharedResponseKeys.TheDeductionHasBeenSuccessfullyRemoved),
                _ => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileDeletingTheDeduction)
            };
        }

        public async Task<ApiResponse> Handle(UpdateDeductionCommand request, CancellationToken cancellationToken)
        {
            var result = await deductionService.UpdateDeductionAsync(request.DeductionId, request.DeductionRate);
            return result switch
            {
                "FinanceEmployeeNotFound" => NotFound(SharedResponseKeys.FinanceEmployeeNotFound),
                "DeductionNotFound" => NotFound(SharedResponseKeys.DeductionNotFound),
                "ModifyingDeductionFailed" => InternalServerError(SharedResponseKeys.ModifyingDeductionFailed),
                "TheDeductionModificationProcessWasCompletedSuccessfully" => 
                Success(new
                {
                    DeductionId = request.DeductionId,
                    DeductionRate = request.DeductionRate,
                }, message: SharedResponseKeys.TheDeductionModificationProcessWasCompletedSuccessfully),
                _ => InternalServerError(SharedResponseKeys.ModifyingDeductionFailed)
            };
        }
    }
}
