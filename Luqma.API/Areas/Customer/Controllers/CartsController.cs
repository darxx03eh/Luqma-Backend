using Luqma.API.Base;
using Luqma.Core.Features.Carts.Commands.Models;
using Luqma.Core.Features.Carts.Queries.Models;
using Luqma.Data.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Luqma.API.Areas.Customer.Controllers
{
  
    [ApiController]
    [Authorize]
    public class CartsController : AppBaseController
    {
        [HttpPost(Router.CustomerCartsRouting.AddToCart)]
        public async Task<IActionResult> AddToCart(AddToCartCommand request)
        {
            var result = await mediator.Send(request);
            return Result(result);
        }
        [HttpGet(Router.CustomerCartsRouting.GetCart)]
        public async Task<IActionResult> GetCartForCustomer()
        {
            var result = await mediator.Send(new GetCartforCustomer());
            return Result(result);
        }
        [HttpPatch(Router.CustomerCartsRouting.IncreaseQuantity)]
        public async Task<IActionResult> IncreaseQuantity(IncreaseQuantityCommand request)
        {
            var result = await mediator.Send(request);
            return Result(result);
        }
        [HttpPatch(Router.CustomerCartsRouting.DecreaseQuantity)]
        public async Task<IActionResult> DecreaseQuantity(DecreaseQuantityCommand request)
        {
            var result = await mediator.Send(request);
            return Result(result);
        }
        [HttpDelete(Router.CustomerCartsRouting.DeleteItemFormCartForCustomerByItemId)]
        public async Task<IActionResult> DeleteItemFromCartForCustomer([FromRoute]int id)//itemid
        {
            var result = await mediator.Send(new DeleteItemFromCartForCustomer(id));
            return Result(result);
        }

    }
}
