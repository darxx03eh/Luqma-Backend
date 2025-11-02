using Luqma.API.Base;
using Luqma.Core.Features.KitchenRequirements.Commands.Models;
using Luqma.Core.Features.KitchenRequirements.Queries.Models;
using Luqma.Data.Helpers;
using Luqma.Data.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Luqma.API.Controllers
{
    [Authorize]
    [ApiController]
    public class KitchenRequirementsController : AppBaseController
    {
        [Authorize(Roles = Roles.Chef)]
        [HttpPost(Router.KitchenRequirementsRouting.PlaceNewKitchenRequirements)]
        public async Task<IActionResult> PlaceNewKitchenRequirements([FromBody] PlaceNewKitchenRequirementsCommand request)
        {
            var result = await mediator.Send(request);
            return Result(result);
        }
        [Authorize(Roles = Roles.Finance)]
        [HttpPatch(Router.KitchenRequirementsRouting.ChangeKitchenRequirementsStatus)]
        public async Task<IActionResult> ChangeKitchenRequirementsStatus([FromBody] ChangeKitchenRequirementsStatusCommand request)
        {
            var result = await mediator.Send(request);
            return Result(result);
        }
        [Authorize(Roles = $"{Roles.Finance},{Roles.Manager},{Roles.Chef}")]
        [HttpGet(Router.KitchenRequirementsRouting.GetPaginatedKitchenRequirements)]
        public async Task<IActionResult> GetPaginatedKitchenRequirements(int pageNumber = 1)
        {
            var result = await mediator.Send(new GetKitchenRequirementsQuery()
            {
                PageNumber = pageNumber
            });
            return Result(result);
        }
        [Authorize(Roles = $"{Roles.Finance},{Roles.Manager},{Roles.Chef}")]
        [HttpGet(Router.KitchenRequirementsRouting.GetKitchenRequirementsById)]
        public async Task<IActionResult> GetKitchenRequirementsById(int id)
        {
            var result = await mediator.Send(new GetKitchenRequirementsByIdQuery(id));
            return Result(result);
        }
        [Authorize(Roles = Roles.Finance)]
        [HttpDelete(Router.KitchenRequirementsRouting.DeleteKitchenRequirements)]
        public async Task<IActionResult> DeleteKitchenRequirements(int id)
        {
            var result = await mediator.Send(new DeleteKitchenRequirementsCommand(id));
            return Result(result);
        }
        [Authorize(Roles = $"{Roles.Finance},{Roles.Manager},{Roles.Chef}")]
        [HttpGet(Router.KitchenRequirementsRouting.GetKitchenRequirementsInfo)]
        public async Task<IActionResult> GetKitchenRequirmentsInfo(int id)
        {
            var result = await mediator.Send(new GetKitchenRequirementsInfoQuery(id));
            return Result(result);
        }
        [Authorize(Roles = Roles.Chef)]
        [HttpDelete(Router.KitchenRequirementsRouting.DeletePendingKitchenRequirements)]
        public async Task<IActionResult> DeletePendingKitchenRequirements(int id)
        {
            var result = await mediator.Send(new DeletePendingRequirementsCommand(id));
            return Result(result);
        }
    }
}
