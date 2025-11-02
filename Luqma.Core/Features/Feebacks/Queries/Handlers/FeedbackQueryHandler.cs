using Luqma.Core.Bases;
using Luqma.Core.Features.Feebacks.Queries.Models;
using Luqma.Core.ResponseKeys;
using Luqma.Service.Interfaces;
using MediatR;

namespace Luqma.Core.Features.Feebacks.Queries.Handlers
{
    public class FeedbackQueryHandler : ApiResponseHandler
        , IRequestHandler<GetFeedbacksForItemCommand, ApiResponse>
    {
        private readonly IFeedbackService feedbackService;

        public FeedbackQueryHandler(IFeedbackService feedbackService)
        {
            this.feedbackService = feedbackService;
        }

        public async Task<ApiResponse> Handle(GetFeedbacksForItemCommand request, CancellationToken cancellationToken)
        {
            var (result, feedbacks) = await feedbackService.GetFeedbacksForItemAsync(request.Id, request.PageNumber, request.PageSize);
            return result switch
            {
                "ItemNotFound" => NotFound(SharedResponseKeys.ItemNotFound),
                "NoFeedbacksFoundForItem" => NotFound(SharedResponseKeys.NoFeedbacksFoundForItem),
                "FeedbacksFoundForItem" => Success(feedbacks, message: SharedResponseKeys.FeedbacksFoundForItem),
                _ => NotFound(SharedResponseKeys.NoFeedbacksFoundForItem)
            };
        }
    }
}
