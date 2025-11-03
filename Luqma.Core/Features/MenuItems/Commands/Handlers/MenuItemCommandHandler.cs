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
        IRequestHandler<AddMenuItemCommand, ApiResponse>,
        IRequestHandler<DeleteMenuItemCommand,ApiResponse>,
        IRequestHandler<UpdateMenuItemCommand,ApiResponse>
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
               
                "the menuitem is added successfully" => Created(null, message:SharedResponseKeys.SuccessAddMenuItem),
                _ => InternalServerError(SharedResponseKeys.AnErrorWhileAddMenuItem)


            };
        }

        public async Task<ApiResponse> Handle(DeleteMenuItemCommand request, CancellationToken cancellationToken)
        {
           
            var result=await _menuItemService.DeleteMenuItemAsync(request.Id);
            return result switch
            {
                "the item Id is not found" => NotFound(SharedResponseKeys.ItemNotFound),
                "An Error while delete photo from  Cloudinary " => InternalServerError(SharedResponseKeys.AnErrorWhileDeletePhotoFromCloudinary),
                "the menu item is deleted successfully" => Deleted(SharedResponseKeys.ItemSuccessDelete),
                "the menu item is not deleted" => InternalServerError(SharedResponseKeys.AnErrorWhileDeleteItem),
                _ => InternalServerError(SharedResponseKeys.AnErrorWhileDeleteItem)

            };
            
        }

        public async Task<ApiResponse> Handle(UpdateMenuItemCommand request, CancellationToken cancellationToken)
        {
            
           var result= await _menuItemService.UpdateMenuItemAsync(request.Id,request.Item,request.Description,request.Discount,request.Price,request.IsVegetarian, request.Image,request.status);
            return result switch
            {
                "the item Id is not found" => NotFound(SharedResponseKeys.ItemNotFound),
                "An Error while delete photo from  Cloudinary " => InternalServerError(SharedResponseKeys.AnErrorWhileDeletePhotoFromCloudinary),
                "the menu item is updated successfully" => Success(null, message: SharedResponseKeys.ItemSuccessUpdate),
                "the menu item is not updated" => InternalServerError(SharedResponseKeys.AnErrorWhileUpdateItem),
                _ => InternalServerError(SharedResponseKeys.AnErrorWhileUpdateItem)

            };
        }
    }
}
