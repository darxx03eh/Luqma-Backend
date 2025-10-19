using Luqma.Core.Bases;
using Luqma.Core.Features.KitchenRequirments.Queries.Models;
using Luqma.Core.ResponseKeys;
using Luqma.Data.Entities;
using Luqma.Service.Interfaces;
using MediatR;

namespace Luqma.Core.Features.KitchenRequirments.Queries.Handlers
{
    public class KitchenRequirmentsQueryHandler : ApiResponseHandler
        , IRequestHandler<GetKitchenRequirementsQuery, ApiResponse>
        , IRequestHandler<GetKitchenRequirementsByIdQuery, ApiResponse>
        , IRequestHandler<GetKitchenRequirmentsInfoQuery, ApiResponse>
    {
        private readonly IKitchenRequirmentsService kitchenRequirmentsService;

        public KitchenRequirmentsQueryHandler(IKitchenRequirmentsService kitchenRequirmentsService)
        {
            this.kitchenRequirmentsService = kitchenRequirmentsService;
        }

        public async Task<ApiResponse> Handle(GetKitchenRequirementsQuery request, CancellationToken cancellationToken)
        {
            var (result, kitchenRequirements) = await kitchenRequirmentsService.GetKitchenRequirmentsAsync(request.PageNumber);
            return result switch
            {
                "FinanceOrManagerNotFound" => NotFound(SharedResponseKeys.FinanceOrManagerNotFound),
                "KitchenRequirmentsNotFound" => NotFound(SharedResponseKeys.KitchenRequirmentsNotFound),
                "KitchenRequirmentsFound" => Success(kitchenRequirements, message: SharedResponseKeys.KitchenRequirmentsFound),
                _ => NotFound(SharedResponseKeys.KitchenRequirmentsNotFound)
            };
        }

        public async Task<ApiResponse> Handle(GetKitchenRequirementsByIdQuery request, CancellationToken cancellationToken)
        {
            var (result, kitchenRequirements) = await kitchenRequirmentsService.GetKitchenRequirmentsByIdAsync(request.Id);
            return result switch
            {
                "FinanceOrManagerNotFound" => NotFound(SharedResponseKeys.FinanceOrManagerNotFound),
                "KitchenRequirmentsNotFound" => NotFound(SharedResponseKeys.KitchenRequirmentsNotFound),
                "KitchenRequirmentsFound" => Success(kitchenRequirements, message: SharedResponseKeys.KitchenRequirmentsFound),
                _ => NotFound(SharedResponseKeys.KitchenRequirmentsNotFound)
            };
        }

        public async Task<ApiResponse> Handle(GetKitchenRequirmentsInfoQuery request, CancellationToken cancellationToken)
        {
            var (result, info) = await kitchenRequirmentsService.GetKitchenRequirmentsInfoAsync(request.Id);
            return result switch
            {
                "FinanceOrManagerNotFound" => NotFound(SharedResponseKeys.FinanceOrManagerNotFound),
                "KitchenRequirmentsNotFound" => NotFound(SharedResponseKeys.KitchenRequirmentsNotFound),
                "RequirmentItemsNotFound" => NotFound(SharedResponseKeys.RequirmentItemsNotFound),
                "KitchenRequirmentsFound" => Success(info, message: SharedResponseKeys.KitchenRequirmentsFound),
                _ => NotFound(SharedResponseKeys.KitchenRequirmentsNotFound)
            };
        }
    }
}
