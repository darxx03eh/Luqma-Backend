using Luqma.API.Base;
using Luqma.Core.Features.Bills.Commands.Models;
using Luqma.Core.Features.Bills.Queries.Models;
using Luqma.Data.Helpers;
using Luqma.Data.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Luqma.API.Controllers
{
    [Authorize]
    [ApiController]
    public class BillsController : AppBaseController
    {
        [Authorize(Roles = Roles.Finance)]
        [HttpPost(Router.BillsRouting.AddBills)]
        public async Task<IActionResult> AddBills([FromBody] AddNewBillCommand request)
        {
            var result = await mediator.Send(request);
            return Result(result);
        }
        [Authorize(Roles = Roles.Finance)]
        [HttpDelete(Router.BillsRouting.DeleteBills)]
        public async Task<IActionResult> DeleteBills(int id)
        {
            var result = await mediator.Send(new DeleteBillCommand(id));
            return Result(result);
        }
        [Authorize(Roles = Roles.Finance)]
        [HttpPatch(Router.BillsRouting.UpdateBillStatus)]
        public async Task<IActionResult> UpdateBillStatus(int id, string status)
        {
            var result = await mediator.Send(new UpdateBillStatusCommand(id, status));
            return Result(result);
        }
        [Authorize(Roles = Roles.Finance)]
        [HttpPut(Router.BillsRouting.UpdateBill)]
        public async Task<IActionResult> UpdateBill([FromBody] UpdateBillCommand request)
        {
            var result = await mediator.Send(request);
            return Result(result);
        }
        [Authorize(Roles = $"{Roles.Finance},{Roles.Manager}")]
        [HttpGet(Router.BillsRouting.GetBills)]
        public async Task<IActionResult> GetBills(int pageNumber = 1)
        {
            var result = await mediator.Send(new GetBillsQuery() { PageNumber = pageNumber });
            return Result(result);
        }
    }
}
