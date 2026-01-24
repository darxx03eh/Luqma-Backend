using Luqma.Core.Bases;
using MediatR;

namespace Luqma.Core.Features.Feebacks.Commands.Models
{
    public class DeleteExistingFeedbackCommand : IRequest<ApiResponse>
    {
        public int Id { get; set; }
        public DeleteExistingFeedbackCommand(int id) => Id = id;
    }
}
