using Luqma.Core.Bases;
using Luqma.Core.Features.KitchenRequirements.Queries.Models;
using Luqma.Core.ResponseKeys;
using Luqma.Data.Entities;
using Luqma.Service.Interfaces;
using MediatR;

namespace Luqma.Core.Features.KitchenRequirements.Queries.Handlers
{
    public class KitchenRequirementsQueryHandler : ApiResponseHandler
        , IRequestHandler<GetKitchenRequirementsQuery, ApiResponse>
        , IRequestHandler<GetKitchenRequirementsByIdQuery, ApiResponse>
        , IRequestHandler<GetKitchenRequirementsInfoQuery, ApiResponse>
    {
        private readonly IKitchenRequirementsService kitchenRequirementsService;

        public KitchenRequirementsQueryHandler(IKitchenRequirementsService kitchenRequirementsService)
        {
            this.kitchenRequirementsService = kitchenRequirementsService;
        }

        public async Task<ApiResponse> Handle(GetKitchenRequirementsQuery request, CancellationToken cancellationToken)
        {
            var (result, kitchenRequirements) = await kitchenRequirementsService.GetKitchenRequirementsAsync(request.PageNumber);
            return result switch
            {
                "FinanceOrManagerNotFound" => NotFound(SharedResponseKeys.FinanceOrManagerNotFound),
                "KitchenRequirementsNotFound" => NotFound(SharedResponseKeys.KitchenRequirementsNotFound),
                "KitchenRequirementsFound" => Success(kitchenRequirements, message: SharedResponseKeys.KitchenRequirementsFound),
                _ => NotFound(SharedResponseKeys.KitchenRequirementsNotFound)
            };
        }

        public async Task<ApiResponse> Handle(GetKitchenRequirementsByIdQuery request, CancellationToken cancellationToken)
        {
            var (result, kitchenRequirements) = await kitchenRequirementsService.GetKitchenRequirementsByIdAsync(request.Id);
            return result switch
            {
                "FinanceOrManagerNotFound" => NotFound(SharedResponseKeys.FinanceOrManagerNotFound),
                "KitchenRequirementsNotFound" => NotFound(SharedResponseKeys.KitchenRequirementsNotFound),
                "KitchenRequirementsFound" => Success(kitchenRequirements, message: SharedResponseKeys.KitchenRequirementsFound),
                _ => NotFound(SharedResponseKeys.KitchenRequirementsNotFound)
            };
        }

        public async Task<ApiResponse> Handle(GetKitchenRequirementsInfoQuery request, CancellationToken cancellationToken)
        {
            var (result, info) = await kitchenRequirementsService.GetKitchenRequirementsInfoAsync(request.Id);
            return result switch
            {
                "FinanceOrManagerNotFound" => NotFound(SharedResponseKeys.FinanceOrManagerNotFound),
                "KitchenRequirementsNotFound" => NotFound(SharedResponseKeys.KitchenRequirementsNotFound),
                "RequirementItemsNotFound" => NotFound(SharedResponseKeys.RequirementItemsNotFound),
                "KitchenRequirementsFound" => Success(info, message: SharedResponseKeys.KitchenRequirementsFound),
                _ => NotFound(SharedResponseKeys.KitchenRequirementsNotFound)
            };
        }
    }
}
