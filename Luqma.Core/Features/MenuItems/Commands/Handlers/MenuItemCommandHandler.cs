using AutoMapper;
using Luqma.Core.Bases;
using Luqma.Core.Features.MenuItems.Commands.Models;
using Luqma.Core.ResponseKeys;
using Luqma.Data.Entities;
using Luqma.Infrastructure.IRepositories;
using Luqma.Service.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Core.Features.MenuItems.Commands.Handlers
{
    public class MenuItemCommandHandler : ApiResponseHandler,
        IRequestHandler<AddMenuItemCommand, ApiResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IMenuItemService _menuItemService;
        private readonly ICategoryItemRepository _categoryItemRepository;

        public MenuItemCommandHandler(IMapper mapper, ICloudinaryService cloudinaryService,IMenuItemService menuItemService)
        {
            _mapper = mapper;
            _cloudinaryService = cloudinaryService;
            _menuItemService = menuItemService;
           
        }
        public async Task<ApiResponse> Handle(AddMenuItemCommand request, CancellationToken cancellationToken)
        {

            var menuitem = _mapper.Map<MenuItem>(request);
           
          var result= await  _menuItemService.AddMenuItemAsync(menuitem, request.Image,request.CategoryId,request.MenuId);
            return result switch
            {
                "the menuitem is added successfully" => Created(null, message:SharedResponseKeys.SuccessMenuItem),
                _ => InternalServerError(SharedResponseKeys.AnErrorWhileAddMenuItem)


            };

            
       
          
            




        }
    }
}
