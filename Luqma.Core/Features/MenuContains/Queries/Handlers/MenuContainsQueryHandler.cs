using AutoMapper;
using Luqma.Core.Bases;
using Luqma.Core.Features.MenuContains.Queries.Models;
using Luqma.Core.ResponseKeys;
using Luqma.Data.Response.MenuContains;
using Luqma.Service.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Core.Features.MenuContains.Queries.Handlers
{
    public class MenuContainsQueryHandler : ApiResponseHandler,
         IRequestHandler<GetitemsbyMenuIdQuery, ApiResponse>

    {
        private readonly IMapper _mapper;
        private readonly IMenuContainService _menuContainService;

        public MenuContainsQueryHandler(IMapper mapper,IMenuContainService menuContainService)
        {
            _mapper = mapper;
            _menuContainService = menuContainService;
        }
        public async Task<ApiResponse> Handle(GetitemsbyMenuIdQuery request, CancellationToken cancellationToken)
        {
          var(result,message)=  await _menuContainService.GetItemsByMenuIdAsync(request.Id);
           var menucontainsRe= _mapper.Map<List<MenuContainsRespons>>(result);
            return message switch
            {
                "the menu id is not found" => NotFound(SharedResponseKeys.NotFoundMenuId),
                "the items is fetched successfully" => Success(menucontainsRe, message: SharedResponseKeys.SuccessGetMenuItems)
            };



        }
    }
}
