using AutoMapper;
using Luqma.Core.Bases;
using Luqma.Core.Features.Menus.Queries.Models;
using Luqma.Core.ResponseKeys;
using Luqma.Data.Response.Menus;
using Luqma.Service.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Core.Features.Menus.Queries.Handlers
{
    public class MenuQueryHandler : ApiResponseHandler,
        IRequestHandler<GetAllMenusQuery, ApiResponse>
    {
        private readonly IMapper _mapper;
        private readonly IMenuService _menuService;

        public MenuQueryHandler(IMapper mapper,IMenuService menuService)
        {
            _mapper = mapper;
            _menuService = menuService;
        }
        public async  Task<ApiResponse> Handle(GetAllMenusQuery request, CancellationToken cancellationToken)
        {
         var (result,message)=  await  _menuService.GetAllMenusAsync();
           var MenusRe= _mapper.Map<List<MenuResponse>>(result);
            return message switch
            {
                "the menus is not found" => NotFound(SharedResponseKeys.NotFoundMenus),
                "the menus is viewed successfully" => Success(MenusRe, message: SharedResponseKeys.SuccessGetMenus)
            };


        }
    }
}
