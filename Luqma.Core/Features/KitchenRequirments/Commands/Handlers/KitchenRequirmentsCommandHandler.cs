using Luqma.Core.Bases;
using Luqma.Core.Features.KitchenRequirments.Commands.Models;
using Luqma.Core.ResponseKeys;
using Luqma.Service.Interfaces;
using MediatR;

namespace Luqma.Core.Features.KitchenRequirments.Commands.Handlers
{
    public class KitchenRequirmentsCommandHandler : ApiResponseHandler
        , IRequestHandler<PlaceNewKitchenRequirmentsCommand, ApiResponse>
    {
        private readonly IKitchenRequirmentsService kitchenRequirmentsService;

        public KitchenRequirmentsCommandHandler(IKitchenRequirmentsService kitchenRequirmentsService)
        {
            this.kitchenRequirmentsService = kitchenRequirmentsService;
        }

        public async Task<ApiResponse> Handle(PlaceNewKitchenRequirmentsCommand request, CancellationToken cancellationToken)
        {
            var result = await kitchenRequirmentsService.PlaceNewKitchenRequirmentsAsync(request.Note, request.RequirmentItems);
            return result switch
            {
                "ChefNotFound" => NotFound(SharedResponseKeys.ChefNotFound),
                "RequirmentItemsNotFound" => NotFound(SharedResponseKeys.RequirmentItemsNotFound),
                "SomeKitchenItemNotFound" => NotFound(SharedResponseKeys.SomeKitchenItemNotFound),
                "AnErrorOccurredWhileAddingKitchenRequirment" =>
                InternalServerError(SharedResponseKeys.AnErrorOccurredWhileAddingKitchenRequirment),
                "KitchenRequirmentAddedSuccessfully" => Success(null, message: SharedResponseKeys.KitchenRequirmentAddedSuccessfully),
                _ => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileAddingKitchenRequirment)
            };
        }
    }
}
