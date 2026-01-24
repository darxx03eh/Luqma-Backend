using AutoMapper;
using Luqma.Core.Bases;
using Luqma.Core.Features.CategoryItems.Queries.Models;
using Luqma.Core.ResponseKeys;
using Luqma.Data.Response.CategoryItems;
using Luqma.Data.Response.MenuItems;
using Luqma.Service.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Core.Features.CategoryItems.Queries.Handler
{
    public class CategoryItemQueryHandler : ApiResponseHandler,
         IRequestHandler<GetItemsbyCategoryIdQuery, ApiResponse>,
        IRequestHandler<GetallMenuItems,ApiResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryItemService _categoryItemService;

        public CategoryItemQueryHandler(IMapper mapper, ICategoryItemService categoryItemService)
        {
            _mapper = mapper;
            _categoryItemService = categoryItemService;
        }
        public async Task<ApiResponse> Handle(GetItemsbyCategoryIdQuery request, CancellationToken cancellationToken)
        {
          var (categoryitems,message)= await  _categoryItemService.GetItemsByCategoryIdAsync(request.Id);
           var CategoryitemsRe= _mapper.Map<List<CategoryItemResponse>>(categoryitems);
            return message switch
            {
                "the category id is not found" => NotFound(SharedResponseKeys.CategoryIdNotFound),
                "the items for category is viewed successfully" => Success(CategoryitemsRe, message: SharedResponseKeys.SuccessViewItemsForCategory)
            };

           
        }

        public async Task<ApiResponse> Handle(GetallMenuItems request, CancellationToken cancellationToken)
        {
          var (result,categoryitems)=  await _categoryItemService.GetAllMenuItemsAsync();
             var menuitemRe=_mapper.Map<List<MenuItemResponse>>(categoryitems);
            return result switch
            {
                "the menuitems is fetched successfully" => Success(menuitemRe,message:SharedResponseKeys.SuccessGetMenuItems)
            };
        }
    }
}
