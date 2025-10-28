using Luqma.API.Base;
using Luqma.Core.Features.Feebacks.Commands.Models;
using Luqma.Data.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Luqma.API.Controllers
{
    [ApiController]
    public class FeedbacksController : AppBaseController
    {
        [Authorize]
        [HttpPost(Router.FeedbacksRouting.AddNewFeedback)]
        public async Task<IActionResult> AddNewFeedback([FromBody] AddNewFeedbackCommand request)
        {
            var result = await mediator.Send(request);
            return Result(result);
        }
    }
}
