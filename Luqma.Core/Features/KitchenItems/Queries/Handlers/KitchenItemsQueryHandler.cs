using Luqma.Core.Bases;
using Luqma.Core.Features.KitchenItems.Queries.Models;
using Luqma.Core.ResponseKeys;
using Luqma.Service.Interfaces;
using MediatR;

namespace Luqma.Core.Features.KitchenItems.Queries.Handlers
{
    public class KitchenItemsQueryHandler : ApiResponseHandler
        , IRequestHandler<GetKitchenItemsQuery, ApiResponse>
        , IRequestHandler<GetKitchenItemByIdQuery, ApiResponse>
    {
        private readonly IKitchenItemsService kitchenItemsService;

        public KitchenItemsQueryHandler(IKitchenItemsService kitchenItemsService)
        {
            this.kitchenItemsService = kitchenItemsService;
        }

        public async Task<ApiResponse> Handle(GetKitchenItemsQuery request, CancellationToken cancellationToken)
        {
            var (result, kitchenItems) = await kitchenItemsService.GetPaginatedKitchenItemsAsync(request.PageNumber, request.Search);
            return result switch
            {
                "ChefOrManagerNotFound" => NotFound(SharedResponseKeys.ChefOrManagerNotFound),
                "KitchenItemsNotFound" => NotFound(SharedResponseKeys.KitchenItemsNotFound),
                "KitchenItemsFound" => Success(kitchenItems, message: SharedResponseKeys.KitchenItemsFound),
                _ => NotFound(SharedResponseKeys.KitchenItemsNotFound)
            };
        }

        public async Task<ApiResponse> Handle(GetKitchenItemByIdQuery request, CancellationToken cancellationToken)
        {
            var (result, kitchenItem) = await kitchenItemsService.GetKitchenItemByIdAsync(request.Id);
            return result switch
            {
                "ChefOrManagerNotFound" => NotFound(SharedResponseKeys.ChefOrManagerNotFound),
                "KitchenItemNotFound" => NotFound(SharedResponseKeys.KitchenItemNotFound),
                "KitchenItemFound" => Success(kitchenItem, message: SharedResponseKeys.KitchenItemFound),
                _ => NotFound(SharedResponseKeys.KitchenItemNotFound)
            };
        }
    }
}
