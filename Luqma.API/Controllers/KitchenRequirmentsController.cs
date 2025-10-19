using Luqma.API.Base;
using Luqma.Core.Features.KitchenRequirments.Commands.Models;
using Luqma.Data.Helpers;
using Luqma.Data.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Luqma.API.Controllers
{
    [Authorize]
    [ApiController]
    public class KitchenRequirmentsController : AppBaseController
    {
        [Authorize(Roles = Roles.Manager)]
        [HttpPost(Router.KitchenRequirmentsRouting.PlaceNewKitchenRequirments)]
        public async Task<IActionResult> PlaceNewKitchenRequirments([FromBody] PlaceNewKitchenRequirmentsCommand request)
        {
            var result = await mediator.Send(request);
            return Result(result);
        }
        [Authorize(Roles = Roles.Finance)]
        [HttpPatch(Router.KitchenRequirmentsRouting.ChangeKitchenRequirmentsStatus)]
        public async Task<IActionResult> ChangeKitchenRequirmentsStatus([FromBody] ChangeKitchenRequirmentsStatusCommand request)
        {
            var result = await mediator.Send(request);
            return Result(result);
        }
    }
}
