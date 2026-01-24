using Luqma.Core.Bases;
using Luqma.Core.Features.KitchenItems.Commands.Models;
using Luqma.Core.ResponseKeys;
using Luqma.Service.Interfaces;
using MediatR;

namespace Luqma.Core.Features.KitchenItems.Commands.Handlers
{
    public class KitchenItemsCommandHandler : ApiResponseHandler
        , IRequestHandler<AddKitchenItemsCommand, ApiResponse>
        , IRequestHandler<DeleteKitchenItemsCommand, ApiResponse>
        , IRequestHandler<UpdateKitchenItemsStatusCommand, ApiResponse>
        , IRequestHandler<UpdateKitchenItemsCommand, ApiResponse>
        , IRequestHandler<UploadNewKitchenItemImageCommand, ApiResponse>
    {
        private readonly IKitchenItemsService kitchenItemsService;

        public KitchenItemsCommandHandler(IKitchenItemsService kitchenItemsService)
        {
            this.kitchenItemsService = kitchenItemsService;
        }

        public async Task<ApiResponse> Handle(AddKitchenItemsCommand request, CancellationToken cancellationToken)
        {
            var (result, kitchenItem) = await kitchenItemsService.AddKitchenItemAsync(
                request.Item, request.Status, request.Image, request.Note, request.Unit, request.Quantity, request.Price
                );
            return result switch
            {
                "ChefNotFound" => NotFound(SharedResponseKeys.ChefNotFound),
                "AnErrorOccurredWhileProcessingItemImageUploadingRequest" => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileProcessingItemImageUploadingRequest),
                "AnErrorOccurredWhileAddingKitchenItem" => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileAddingKitchenItem),
                "KitchenItemAddedSuccessfully" => Success(kitchenItem, message: SharedResponseKeys.KitchenItemAddedSuccessfully),
                "AnErrorOccurredWhileAddingItemImage" => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileAddingItemImage),
                _ => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileAddingKitchenItem)
            };
        }

        public async Task<ApiResponse> Handle(DeleteKitchenItemsCommand request, CancellationToken cancellationToken)
        {
            var result = await kitchenItemsService.DeleteKitchenItemAsync(request.Id);
            return result switch
            {
                "ChefNotFound" => NotFound(SharedResponseKeys.ChefNotFound),
                "KitchenItemNotFound" => NotFound(SharedResponseKeys.KitchenItemNotFound),
                "AnErrorOccurredWhileDeletingKitchenItem" =>
                InternalServerError(SharedResponseKeys.AnErrorOccurredWhileDeletingKitchenItem),
                "KitchenItemDeletedSuccessfully" => Success(null, message: SharedResponseKeys.KitchenItemDeletedSuccessfully),
                _ => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileDeletingKitchenItem)
            };
        }

        public async Task<ApiResponse> Handle(UpdateKitchenItemsStatusCommand request, CancellationToken cancellationToken)
        {
            var result = await kitchenItemsService.UpdateItemStatusAsync(request.Id, request.Status);
            return result switch
            {
                "ChefNotFound" => NotFound(SharedResponseKeys.ChefNotFound),
                "KitchenItemNotFound" => NotFound(SharedResponseKeys.KitchenItemNotFound),
                "AnErrorOccurredWhileUpdatingKitchenItemStatus" =>
                InternalServerError(SharedResponseKeys.AnErrorOccurredWhileUpdatingKitchenItemStatus),
                "KitchenItemStatusUpdatingSuccessfully" => Success(new
                {
                    NewStatus = request.Status
                }, message: SharedResponseKeys.KitchenItemStatusUpdatingSuccessfully),
                _ => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileUpdatingKitchenItemStatus)
            };
        }

        public async Task<ApiResponse> Handle(UpdateKitchenItemsCommand request, CancellationToken cancellationToken)
        {
            var (result, kitchenItem) = await kitchenItemsService.UpdateKitchenItemAsync(request.Id,
                request.Item, request.Status, request.Note, request.Unit, request.Quantity, request.Price
                );
            return result switch
            {
                "ChefNotFound" => NotFound(SharedResponseKeys.ChefNotFound),
                "KitchenItemNotFound" => NotFound(SharedResponseKeys.KitchenItemNotFound),
                "FailedToDeleteImageFromCloudinary" => InternalServerError(SharedResponseKeys.FailedToDeleteImageFromCloudinary),
                "AnErrorOccurredWhileDeletingOldItemImage" =>
                InternalServerError(SharedResponseKeys.AnErrorOccurredWhileDeletingOldItemImage),
                "AnErrorOccurredWhileUpdatingKitchenItem" =>
                InternalServerError(SharedResponseKeys.AnErrorOccurredWhileUpdatingKitchenItem),
                "KitchenItemUpdatingSuccessfully" => Success(kitchenItem, message: SharedResponseKeys.KitchenItemUpdatingSuccessfully),
                _ => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileUpdatingKitchenItem)
            };
        }

        public async Task<ApiResponse> Handle(UploadNewKitchenItemImageCommand request, CancellationToken cancellationToken)
        {
            var (result, imageUrl) = await kitchenItemsService.UploadItemImageAsync(request.Id, request.Image);
            return result switch
            {
                "ChefNotFound" => NotFound(SharedResponseKeys.ChefNotFound),
                "KitchenItemNotFound" => NotFound(SharedResponseKeys.KitchenItemNotFound),
                "FailedToDeleteImageFromCloudinary" => InternalServerError(SharedResponseKeys.FailedToDeleteImageFromCloudinary),
                "AnErrorOccurredWhileProcessingImageModificationRequest" => 
                InternalServerError(SharedResponseKeys.AnErrorOccurredWhileProcessingImageModificationRequest),
                "TheImageHasBeenChangedSuccessfully" => Success(new
                {
                    ImageUrl = imageUrl,
                }, message: SharedResponseKeys.TheImageHasBeenChangedSuccessfully),
                _ => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileProcessingImageModificationRequest)
            };
        }
    }
}
