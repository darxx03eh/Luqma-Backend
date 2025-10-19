using Luqma.API.Base;
using Luqma.Core.Features.KitchenRequirments.Commands.Models;
using Luqma.Core.Features.KitchenRequirments.Queries.Models;
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
        [Authorize(Roles = Roles.Chef)]
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
        [Authorize(Roles = $"{Roles.Finance},{Roles.Manager}")]
        [HttpGet(Router.KitchenRequirmentsRouting.GetPaginatedKitchenRequirements)]
        public async Task<IActionResult> GetPaginatedKitchenRequirements(int pageNumber = 1)
        {
            var result = await mediator.Send(new GetKitchenRequirementsQuery()
            {
                PageNumber = pageNumber
            });
            return Result(result);
        }
        [Authorize(Roles = $"{Roles.Finance},{Roles.Manager}")]
        [HttpGet(Router.KitchenRequirmentsRouting.GetKitchenRequirementsById)]
        public async Task<IActionResult> GetKitchenRequirementsById(int id)
        {
            var result = await mediator.Send(new GetKitchenRequirementsByIdQuery(id));
            return Result(result);
        }
        [Authorize(Roles = Roles.Chef)]
        [HttpDelete(Router.KitchenRequirmentsRouting.DeleteKitchenRequirments)]
        public async Task<IActionResult> DeleteKitchenRequirments(int id)
        {
            var result = await mediator.Send(new DeleteKitchenRequirmentsCommand(id));
            return Result(result);
        }
    }
}
