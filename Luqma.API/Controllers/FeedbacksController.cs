using Luqma.API.Base;
using Luqma.Core.Features.Feebacks.Commands.Models;
using Luqma.Core.Features.Feebacks.Queries.Models;
using Luqma.Data.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Luqma.API.Controllers
{
    [Authorize]
    [ApiController]
    public class FeedbacksController : AppBaseController
    {
        [HttpPost(Router.FeedbacksRouting.AddNewFeedback)]
        public async Task<IActionResult> AddNewFeedback([FromBody] AddNewFeedbackCommand request)
        {
            var result = await mediator.Send(request);
            return Result(result);
        }
        [HttpPut(Router.FeedbacksRouting.UpdateExistingFeedback)]
        public async Task<IActionResult> UpdateExistingFeedback([FromBody] UpdateExistingFeedbackCommand request)
        {
            var result = await mediator.Send(request);
            return Result(result);
        }
        [AllowAnonymous]
        [HttpGet(Router.FeedbacksRouting.GetFeedbacksForItem)]
        public async Task<IActionResult> GetFeedbacksForItem(int id, int pageNumber = 1, int pageSize = 10)
        {
            Console.WriteLine(DateTime.UtcNow);
            var result = await mediator.Send(new GetFeedbacksForItemCommand()
            {
                Id = id,
                PageNumber = pageNumber,
                PageSize = pageSize
            });
            return Result(result);
        }
        [HttpDelete(Router.FeedbacksRouting.DeleteExistingFeedback)]
        public async Task<IActionResult> DeleteExistingFeedback(int id)
        {
            var result = await mediator.Send(new DeleteExistingFeedbackCommand(id));
            return Result(result);
        }
    }
}
