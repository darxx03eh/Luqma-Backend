using Luqma.API.Base;
using Luqma.Core.Features.Categories.Commands.Handlers;
using Luqma.Core.Features.Categories.Commands.Models;
using Luqma.Core.Features.Categories.Queries.Models;
using Luqma.Data.Routing;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Twilio.Rest.FlexApi.V1;

namespace Luqma.API.Areas.Manager.Controllers
{
    
    [ApiController]
   // [Authorize(Roles ="Manager")]
    public class CategoriesController :AppBaseController
    {
        [HttpGet(Router.ManagerCategoriesRouting.GetAll)]
       
        public async Task<IActionResult> GetAll()
        {
            var result =await  mediator.Send(new GetAllCategoryQuery());
            return Result(result);
        }
        [HttpGet(Router.ManagerCategoriesRouting.GetById)]
     public async Task<IActionResult> GetById([FromRoute]int id)
        {
            var result = await mediator.Send(new GetCategoryQuery(id));
            return Result(result);

        }
       
        
        [HttpPost(Router.ManagerCategoriesRouting.Add)]
    public async Task<IActionResult> Create([FromBody]AddCategoryCommand request)
        {
             var result=await mediator.Send(request);
            return Result(result);
        }
        [HttpPatch(Router.ManagerCategoriesRouting.Update)]
       
        public async Task<IActionResult> Update([FromBody]UpdateCategoryCommand request)
        {
            var result = await mediator.Send(request);
            return Result(result);
        }
        [HttpDelete(Router.ManagerCategoriesRouting.Delete)]
       
        public async Task<IActionResult>Delete([FromRoute]int id)
        {
            var result = await mediator.Send(new DeleteCategoryCommand(id));
            return Result(result);
            
        }





    }
}
