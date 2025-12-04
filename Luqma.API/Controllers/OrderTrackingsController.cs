using Luqma.API.Base;
using Luqma.Core.Features.OrdersTracking.Queries.Models;
using Luqma.Data.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Luqma.API.Controllers
{
    [Authorize]
    [ApiController]
    public class OrderTrackingsController : AppBaseController
    {
        [HttpGet(Router.OrdersTrackingRouting.TrackOrder)]
        public async Task<IActionResult> TrackOrder(int id)
        {
            var result = await mediator.Send(new OrderTrackingQuery(id));
            return Result(result);
        }
    }
}