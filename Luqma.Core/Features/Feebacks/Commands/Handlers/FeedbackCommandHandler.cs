using Luqma.Core.Bases;
using Luqma.Core.Features.Feebacks.Commands.Models;
using Luqma.Core.ResponseKeys;
using Luqma.Service.Interfaces;
using MediatR;

namespace Luqma.Core.Features.Feebacks.Commands.Handlers
{
    public class FeedbackCommandHandler : ApiResponseHandler
        , IRequestHandler<AddNewFeedbackCommand, ApiResponse>
        , IRequestHandler<UpdateExistingFeedbackCommand, ApiResponse>
        , IRequestHandler<DeleteExistingFeedbackCommand, ApiResponse>
    {
        private readonly IFeedbackService feedbackService;

        public FeedbackCommandHandler(IFeedbackService feedbackService)
        {
            this.feedbackService = feedbackService;
        }

        public async Task<ApiResponse> Handle(AddNewFeedbackCommand request, CancellationToken cancellationToken)
        {
            var (result, feedback, itemTotalStars) = await feedbackService.AddNewFeedbackAsync(request.ItemId, request.Stars, request.Content);
            return result switch
            {
                "CustomerNotFound" => NotFound(SharedResponseKeys.CustomerNotFound),
                "ItemNotFound" => NotFound(SharedResponseKeys.ItemNotFound),
                "YouHaveNoOrders" => NotFound(SharedResponseKeys.YouHaveNoOrders),
                "YouHaveNoOrdersWithThisItem" => NotFound(SharedResponseKeys.YouHaveNoOrdersWithThisItem),
                "YouAlreadyRatedThisItem" => Conflict(SharedResponseKeys.YouAlreadyRatedThisItem),
                "AnErrorOccurredWhileAddingTheFeedback" => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileAddingTheFeedback),
                "AnErrorOccurredWhileUpdatingTheTotalStars" => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileUpdatingTheTotalStars),
                "FeedbackAddedSuccessfully" => Success(feedback, new
                {
                    ItemTotalStars = itemTotalStars
                }, SharedResponseKeys.FeedbackAddedSuccessfully),
                _ => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileAddingTheFeedback)
            };
        }

        public async Task<ApiResponse> Handle(UpdateExistingFeedbackCommand request, CancellationToken cancellationToken)
        {
            var (result, feedback, itemTotalStars) = await feedbackService.UpdateExistingFeedbackAsync(request.FeedbackId, request.Stars, request.Content);
            return result switch
            {
                "CustomerNotFound" => NotFound(SharedResponseKeys.CustomerNotFound),
                "FeedbackNotFound" => NotFound(SharedResponseKeys.FeedbackNotFound),
                "FeedbacksForCustomerNotFound" => NotFound(SharedResponseKeys.FeedbacksForCustomerNotFound),
                "ThisFeedbackDoNotBelongToThisCustomer" => Forbidden(SharedResponseKeys.ThisFeedbackDoNotBelongToThisCustomer),
                "ItemNotFound" => NotFound(SharedResponseKeys.ItemNotFound),
                "AnErrorOccurredWhileUpdatingTheTotalStars" => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileUpdatingTheTotalStars),
                "AnErrorOccurredWhileUpdatingFeedback" => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileUpdatingFeedback),
                "TheFeedbackWasSuccessfullyUpdated" => Success(feedback, new
                {
                    ItemTotalStars = itemTotalStars
                }, SharedResponseKeys.TheFeedbackWasSuccessfullyUpdated),
                _ => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileUpdatingFeedback)
            };
        }

        public async Task<ApiResponse> Handle(DeleteExistingFeedbackCommand request, CancellationToken cancellationToken)
        {
            var result = await feedbackService.DeleteExistingFeedbackAsync(request.Id);
            return result switch
            {
                "CustomerNotFound" => NotFound(SharedResponseKeys.CustomerNotFound),
                "FeedbackNotFound" => NotFound(SharedResponseKeys.FeedbackNotFound),
                "FeedbacksForCustomerNotFound" => NotFound(SharedResponseKeys.FeedbacksForCustomerNotFound),
                "ThisFeedbackDoNotBelongToThisCustomer" => Forbidden(SharedResponseKeys.ThisFeedbackDoNotBelongToThisCustomer),
                "AnErrorOccurredWhileUpdatingTheTotalStars" => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileUpdatingTheTotalStars),
                "ItemNotFound" => NotFound(SharedResponseKeys.ItemNotFound),
                "AnErrorOccurredWhileDeletingFeedback" => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileDeletingFeedback),
                "TheFeedbackWasSuccessfullyDeleted" => Success(null, message: SharedResponseKeys.TheFeedbackWasSuccessfullyDeleted),
                _ => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileDeletingFeedback)
            };
        }
    }
}
