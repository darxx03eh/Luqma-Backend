using Luqma.Core.Bases;
using Luqma.Core.Features.Feebacks.Commands.Models;
using Luqma.Core.ResponseKeys;
using Luqma.Service.Interfaces;
using MediatR;

namespace Luqma.Core.Features.Feebacks.Commands.Handlers
{
    public class FeedbackCommandHandler : ApiResponseHandler
        , IRequestHandler<AddNewFeedbackCommand, ApiResponse>
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
                "NoFeedBacksForThisItemFound" => NotFound(SharedResponseKeys.NoFeedBacksForThisItemFound),
                "FeedbackAddedSuccessfully" => Success(feedback, new
                {
                    ItemTotalStars = itemTotalStars
                }, SharedResponseKeys.FeedbackAddedSuccessfully),
                _ => InternalServerError(SharedResponseKeys.AnErrorOccurredWhileAddingTheFeedback)
            };
        }
    }
}
