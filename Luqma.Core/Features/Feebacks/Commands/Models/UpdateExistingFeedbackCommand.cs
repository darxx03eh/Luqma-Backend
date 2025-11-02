using Luqma.Core.Bases;
using MediatR;

namespace Luqma.Core.Features.Feebacks.Commands.Models
{
    public class UpdateExistingFeedbackCommand : IRequest<ApiResponse>
    {
        public int FeedbackId { get; set; }
        public double Stars { get; set; }
        public string Content { get; set; }
    }
}
