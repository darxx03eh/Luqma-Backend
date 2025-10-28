using Luqma.Core.Bases;
using MediatR;

namespace Luqma.Core.Features.Feebacks.Commands.Models
{
    public class AddNewFeedbackCommand : IRequest<ApiResponse>
    {
        public int ItemId { get; set; }
        public double Stars { get; set; }
        public string? Content { get; set; }
    }
}
