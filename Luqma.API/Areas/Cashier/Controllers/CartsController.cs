using CloudinaryDotNet.Actions;
using Luqma.API.Base;
using Luqma.Core.Features.Carts.Commands.Models;
using Luqma.Core.Features.Carts.Queries.Models;
using Luqma.Data.Helpers;
using Luqma.Data.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Luqma.API.Areas.Cashier.Controllers
{
   
    [ApiController]
    [Authorize(Roles =Roles.Cashier)]
    public class CartsController : AppBaseController
    {
        [HttpPost(Router.CashierCartsRouting.AddToCart)]
        public async Task<IActionResult> AddToCart(AddToCartCommand request)
        {
            var result = await mediator.Send(request);
            return Result(result);
        }
        [HttpGet(Router.CashierCartsRouting.GetCart)]
        public async Task<IActionResult> GetCartForCashier()
        {
            var result = await mediator.Send(new GetCartforCustomer());
            return Result(result);
        }
        [HttpPatch(Router.CashierCartsRouting.IncreaseQuantity)]
        public async Task<IActionResult> IncreaseQuantity(IncreaseQuantityCommand request)
        {
            var result = await mediator.Send(request);
            return Result(result);
        }
        [HttpPatch(Router.CashierCartsRouting.DecreaseQuantity)]
        public async Task<IActionResult> DecreaseQuantity(DecreaseQuantityCommand request)
        {
            var result = await mediator.Send(request);
            return Result(result);
        }
        [HttpDelete(Router.CashierCartsRouting.DeleteItemFormCartForCashierByItemId)]
        public async Task<IActionResult> DeleteItemFromCartForCashier([FromRoute] int id)//itemid
        {
            var result = await mediator.Send(new DeleteItemFromCartForCustomer(id));
            return Result(result);
        }

    }
}
