using Luqma.Core.Bases;
using Luqma.Core.Features.Bills.Commands.Models;
using Luqma.Core.ResponseKeys;
using Luqma.Service.Interfaces;
using MediatR;

namespace Luqma.Core.Features.Bills.Commands.Handlers
{
    public class BillCommandHandler : ApiResponseHandler
        , IRequestHandler<AddNewBillCommand, ApiResponse>
        , IRequestHandler<DeleteBillCommand, ApiResponse>
        , IRequestHandler<UpdateBillStatusCommand, ApiResponse>
        , IRequestHandler<UpdateBillCommand, ApiResponse>
    {
        private readonly IBillService billService;

        public BillCommandHandler(IBillService billService)
        {
            this.billService = billService;
        }
        public async Task<ApiResponse> Handle(AddNewBillCommand request, CancellationToken cancellationToken)
        {
            var (result, bill) = await billService.AddNewBillAsync(request.BillType, request.TotalPrice, request.Note);
            return result switch
            {
                "FinanceEmployeeNotFound" => NotFound(SharedResponseKeys.FinanceEmployeeNotFound),
                "AnErrorOccurredWhileAddingTheBill" => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileAddingTheBill),
                "TheBillHasBeenAddedSuccessfully" => Success(bill, message: SharedResponseKeys.TheBillHasBeenAddedSuccessfully),
                _ => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileAddingTheBill)
            };
        }

        public async Task<ApiResponse> Handle(DeleteBillCommand request, CancellationToken cancellationToken)
        {
            var result = await billService.DeleteBillAsync(request.Id);
            return result switch
            {
                "FinanceEmployeeNotFound" => NotFound(SharedResponseKeys.FinanceEmployeeNotFound),
                "BillNotFound" => NotFound(SharedResponseKeys.BillNotFound),
                "AnErrorOccurredWhileDeletingTheBill" => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileDeletingTheBill),
                "TheBillHasBeenDeletedSuccessfully" => Success(null, message: SharedResponseKeys.TheBillHasBeenDeletedSuccessfully),
                _ => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileDeletingTheBill)
            };
        }

        public async Task<ApiResponse> Handle(UpdateBillStatusCommand request, CancellationToken cancellationToken)
        {
            var result = await billService.UpdateBillStatusAsync(request.Id, request.Status);
            return result switch
            {
                "FinanceEmployeeNotFound" => NotFound(SharedResponseKeys.FinanceEmployeeNotFound),
                "BillNotFound" => NotFound(SharedResponseKeys.BillNotFound),
                "AnErrorOccurredWhileEditingTheStatus" => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileEditingTheStatus),
                "TheStatusHasBeenModifiedSuccessfully" => Success(null, message: SharedResponseKeys.TheStatusHasBeenModifiedSuccessfully),
                _ => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileEditingTheStatus)
            };
        }

        public async Task<ApiResponse> Handle(UpdateBillCommand request, CancellationToken cancellationToken)
        {
            var result = await billService.UpdateBillAsync(request.Id, request.BillType, request.Note, request.TotalPrice);
            return result switch
            {
                "FinanceEmployeeNotFound" => NotFound(SharedResponseKeys.FinanceEmployeeNotFound),
                "BillNotFound" => NotFound(SharedResponseKeys.BillNotFound),
                "AnErrorOccurredWhileEditingTheBill" => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileEditingTheBill),
                "TheBillHasBeenModifiedSuccessfully" => Success(null, message: SharedResponseKeys.TheBillHasBeenModifiedSuccessfully),
                _ => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileEditingTheBill)
            };
        }
    }
}
