using Luqma.Core.Bases;
using Luqma.Core.Features.KitchenRequirements.Commands.Models;
using Luqma.Core.ResponseKeys;
using Luqma.Service.Interfaces;
using MediatR;

namespace Luqma.Core.Features.KitchenRequirements.Commands.Handlers
{
    public class KitchenRequirementsCommandHandler : ApiResponseHandler
        , IRequestHandler<PlaceNewKitchenRequirementsCommand, ApiResponse>
        , IRequestHandler<ChangeKitchenRequirementsStatusCommand, ApiResponse>
        , IRequestHandler<DeleteKitchenRequirementsCommand, ApiResponse>
        , IRequestHandler<DeletePendingRequirementsCommand, ApiResponse>
    {
        private readonly IKitchenRequirementsService kitchenRequirementsService;

        public KitchenRequirementsCommandHandler(IKitchenRequirementsService kitchenRequirementsService)
        {
            this.kitchenRequirementsService = kitchenRequirementsService;
        }

        public async Task<ApiResponse> Handle(PlaceNewKitchenRequirementsCommand request, CancellationToken cancellationToken)
        {
            var (result, requirement) = await kitchenRequirementsService.PlaceNewKitchenRequirementsAsync(request.Note, request.RequirementItems);
            return result switch
            {
                "ChefNotFound" => NotFound(SharedResponseKeys.ChefNotFound),
                "RequirementItemsNotFound" => NotFound(SharedResponseKeys.RequirementItemsNotFound),
                "SomeKitchenItemNotFound" => NotFound(SharedResponseKeys.SomeKitchenItemNotFound),
                "AnErrorOccurredWhileAddingKitchenRequirement" =>
                InternalServerError(SharedResponseKeys.AnErrorOccurredWhileAddingKitchenRequirement),
                "KitchenRequirementAddedSuccessfully" => Success(requirement, message: SharedResponseKeys.KitchenRequirementAddedSuccessfully),
                _ => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileAddingKitchenRequirement)
            };
        }

        public async Task<ApiResponse> Handle(ChangeKitchenRequirementsStatusCommand request, CancellationToken cancellationToken)
        {
            var result = await kitchenRequirementsService.ChangeKitchenRequirementsAsync(request.Id, request.Status);
            return result switch
            {
                "FinanceEmployeeNotFound" => NotFound(SharedResponseKeys.FinanceEmployeeNotFound),
                "KitchenRequirementsNotFound" => NotFound(SharedResponseKeys.KitchenRequirementsNotFound),
                "AnErrorOccurredWhileEditingTheStatus" => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileEditingTheStatus),
                "YouCanNotChangeStatusForThisKitchenRequirementsAlreadyAccepted" =>
                Forbidden(SharedResponseKeys.YouCanNotChangeStatusForThisKitchenRequirementsAlreadyAccepted),
                "YouCanNotChangeStatusForThisKitchenRequirementsAlreadyRejected" => 
                Forbidden(SharedResponseKeys.YouCanNotChangeStatusForThisKitchenRequirementsAlreadyRejected),
                "TheStatusHasBeenModifiedSuccessfully" => Success(new
                {
                    NewStatus = request.Status,
                }, message: SharedResponseKeys.TheStatusHasBeenModifiedSuccessfully),
                _ => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileEditingTheStatus)
            };
        }

        public async Task<ApiResponse> Handle(DeleteKitchenRequirementsCommand request, CancellationToken cancellationToken)
        {
            var result = await kitchenRequirementsService.DeleteKitchenRequirementsAsync(request.Id);
            return result switch
            {
                "FinanceEmployeeNotFound" => NotFound(SharedResponseKeys.FinanceEmployeeNotFound),
                "KitchenRequirementsNotFound" => NotFound(SharedResponseKeys.KitchenRequirementsNotFound),
                "AnErrorOccurredWhileDeletingKitchenRequirements" =>
                InternalServerError(SharedResponseKeys.AnErrorOccurredWhileDeletingKitchenRequirements),
                "KitchenRequirementsDeletedSuccessfully" => Success(null, message: SharedResponseKeys.KitchenRequirementsDeletedSuccessfully),
                _ => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileDeletingKitchenRequirements)
            };
        }

        public async Task<ApiResponse> Handle(DeletePendingRequirementsCommand request, CancellationToken cancellationToken)
        {
            var result = await kitchenRequirementsService.DeletePendingKitchenRequirementsAsync(request.Id);
            return result switch
            {
                "ChefNotFound" => NotFound(SharedResponseKeys.ChefNotFound),
                "KitchenRequirementsNotFound" => NotFound(SharedResponseKeys.KitchenRequirementsNotFound),
                "CanNotDeleteNonPendingKitchenRequirements" => Forbidden(SharedResponseKeys.CanNotDeleteNonPendingKitchenRequirements),
                "AnErrorOccurredWhileDeletingKitchenRequirements" => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileDeletingKitchenRequirements),
                "KitchenRequirementsDeletedSuccessfully" => Success(null, message: SharedResponseKeys.KitchenRequirementsDeletedSuccessfully),
                _ => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileDeletingKitchenRequirements)
            };
        }
    }
}
