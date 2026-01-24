using AutoMapper;
using Luqma.Core.Bases;
using Luqma.Core.Features.Categories.Queries.Models;
using Luqma.Core.Features.MenuItems.Queries.Models;
using Luqma.Core.ResponseKeys;
using Luqma.Data.Response.MenuItems;
using Luqma.Service.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Core.Features.MenuItems.Queries.Handlers
{
    public class MenuItemQueryHandler : ApiResponseHandler,
        IRequestHandler<GetAllMenuItemQuery, ApiResponse>,
        IRequestHandler<GetMenuItemByIdQuery,ApiResponse>
    {
        private readonly IMapper _mapper;
        private readonly IMenuItemService _menuItemService;

        public MenuItemQueryHandler(IMapper mapper,IMenuItemService menuItemService)
        {
            _mapper = mapper;
            _menuItemService = menuItemService;
        }
        public async Task<ApiResponse> Handle(GetAllMenuItemQuery request, CancellationToken cancellationToken)
        {
            var (result, message) = await _menuItemService.GetAllAsync();
            var MenuitemsRe = _mapper.Map<List<MenuItemResponse>>(result);
            return message switch
            {
                "the menuitems is not found" => NotFound(SharedResponseKeys.MenuItemsNotFound),
                "the menuItems is viewed successfully" => Success(MenuitemsRe, message: SharedResponseKeys.SuccessGetMenuItems),

            };


        }

        public async  Task<ApiResponse> Handle(GetMenuItemByIdQuery request, CancellationToken cancellationToken)
        {
             var (item,result)=await _menuItemService.GetByIdAsync(request.Id);
             var itemResponse=_mapper.Map<MenuItemResponse>(item);
            return result switch
            {
                "the item is fetched successfully" => Success(itemResponse, SharedResponseKeys.ItemFetchSuccess)
            };
        }
    }
}
