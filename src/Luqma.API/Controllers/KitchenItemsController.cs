using Luqma.API.Base;
using Luqma.Core.Features.KitchenItems.Commands.Models;
using Luqma.Core.Features.KitchenItems.Queries.Models;
using Luqma.Data.Helpers;
using Luqma.Data.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Luqma.API.Controllers
{
    [Authorize]
    [ApiController]
    public class KitchenItemsController : AppBaseController
    {
        [Authorize(Roles = $"{Roles.Chef},{Roles.Manager}")]
        [HttpGet(Router.KitchenItemsRouting.GetKitchenItems)]
        public async Task<IActionResult> GetKitchenItems(string? search, int pageNumber = 1)
        {
            var result = await mediator.Send(new GetKitchenItemsQuery
            {
                PageNumber = pageNumber,
                Search = search
            });
            return Result(result);
        }
        [Authorize(Roles = Roles.Chef)]
        [HttpPost(Router.KitchenItemsRouting.AddKitchenItems)]
        public async Task<IActionResult> AddKitchenItems([FromForm] AddKitchenItemsCommand request)
        {
            var result = await mediator.Send(request);
            return Result(result);
        }
        [Authorize(Roles = $"{Roles.Chef},{Roles.Manager}")]
        [HttpGet(Router.KitchenItemsRouting.GetKitchenItemById)]
        public async Task<IActionResult> GetKitchenItemById(int id)
        {
            var result = await mediator.Send(new GetKitchenItemByIdQuery(id));
            return Result(result);
        }
        [Authorize(Roles = Roles.Chef)]
        [HttpDelete(Router.KitchenItemsRouting.DeleteKitchenItem)]
        public async Task<IActionResult> DeleteKitchenItem(int id)
        {
            var result = await mediator.Send(new DeleteKitchenItemsCommand(id));
            return Result(result);
        }
        [Authorize(Roles = Roles.Chef)]
        [HttpPatch(Router.KitchenItemsRouting.UpdateKitchenItemStatus)]
        public async Task<IActionResult> UpdateKitchenItemStatus([FromBody] UpdateKitchenItemsStatusCommand request)
        {
            var result = await mediator.Send(request);
            return Result(result);
        }
        [Authorize(Roles = Roles.Chef)]
        [HttpPut(Router.KitchenItemsRouting.UpdateKitchenItem)]
        public async Task<IActionResult> UpdateKitchenItem([FromBody] UpdateKitchenItemsCommand request)
        {
            var result = await mediator.Send(request);
            return Result(result);
        }
        [Authorize(Roles = Roles.Chef)]
        [HttpPatch(Router.KitchenItemsRouting.UploadKitchenItemImage)]
        public async Task<IActionResult> UploadKitchenItemImage([FromForm] UploadNewKitchenItemImageCommand request)
        {
            var result = await mediator.Send(request);
            return Result(result);
        }
    }
}
