using Luqma.Data.Entities;
using Luqma.Infrastructure.IRepositories;
using Luqma.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Luqma.Service.Implementations
{
   public  class MenuItemService:IMenuItemService
    {
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly ICategoryItemRepository _categoryItemRepository;
        private readonly IMenuContainsRepository _menuContainsRepository;

        public MenuItemService(IMenuItemRepository menuItemRepository 
            ,ICloudinaryService cloudinaryService
           )
        {
            _menuItemRepository = menuItemRepository;
            _cloudinaryService = cloudinaryService;
          
        }
        public async Task<string> AddMenuItemAsync(MenuItem menuItem,IFormFile file, ICollection<int> CategoryId,ICollection<int>MenuId)
        {
           
            if (file != null)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var imageurl = await _cloudinaryService.UploadFileAsync(file.OpenReadStream(), "Luqma/MenuItems", fileName);
                menuItem.ImageUrl = imageurl;
            }
         
            foreach(var cat in CategoryId)
            {
                menuItem.CategoryItems.Add(new CategoryItem()
                {
                    CategoryId = cat
                });
            }
           
            foreach (var men in MenuId)
            {
               menuItem.MenuContains.Add(new MenuContains()
                {
                   
                    MenuId = men
                });
            }

           await  _menuItemRepository.AddAsync(menuItem);
            return "the menuitem is added successfully";  

        }
        public async Task<string> DeleteMenuItemAsync(MenuItem menuItem)
        {
            var item = await _menuItemRepository.GetByIdAsync(menuItem.Id);
            if (item is null) return "the item Id is not found";
            if (item.ImageUrl != null)
            {

                var result = await _cloudinaryService.DeleteFileAsync(item.ImageUrl);

                if (result.Equals("FailedToDeleteImageFromCloudinary") || result.Equals("AnErrorOccurredWhileDeletingFromCloudinary"))
                    return "An Error while delete photo from  Cloudinary ";
            }


                var count = await _menuItemRepository.DeleteAsync(item);
           
            return count > 0 ? "the menu item is deleted successfully" : "the menu item is not deleted";

        }
        public async Task<string> UpdateMenuItemAsync(MenuItem menuItem,IFormFile? file)
        {
            
            var item=await _menuItemRepository.GetByIdAsync(menuItem.Id);
            if (item is null) return "the item Id is not found";
            item.Item = menuItem.Item;
            item.Description = menuItem.Description;
            item.Price = menuItem.Price;
            item.Discount = menuItem.Discount;
            item.IsVegetarian = menuItem.IsVegetarian;

           
            if (file == null && item.ImageUrl!=null)
            {
                var result = await _cloudinaryService.DeleteFileAsync(item.ImageUrl);
                if (result.Equals("FailedToDeleteImageFromCloudinary") || result.Equals("AnErrorOccurredWhileDeletingFromCloudinary"))
                    return "An Error while delete photo from  Cloudinary ";
                item.ImageUrl = null;

                
            }

                
            if ( file!=null && item.ImageUrl != null)
            {
               var result= await _cloudinaryService.DeleteFileAsync(item.ImageUrl);
                if (result.Equals("FailedToDeleteImageFromCloudinary") || result.Equals("AnErrorOccurredWhileDeletingFromCloudinary"))
                    return "An Error while delete photo from  Cloudinary ";

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var fileurl = await _cloudinaryService.UploadFileAsync(file.OpenReadStream(), "Luqma/MenuItems", fileName);
                item.ImageUrl = fileurl;
                
            }
            if(file!=null && item.ImageUrl==null)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var fileurl = await _cloudinaryService.UploadFileAsync(file.OpenReadStream(), "Luqma/MenuItems", fileName);
                item.ImageUrl = fileurl;
            }
         


                var count = await _menuItemRepository.UpdateAsync(item);
            return count > 0 ? "the menu item is updated successfully" : "the menu item is not updated";
          


        }
        public async Task<(IQueryable<MenuItem>?, string)> GetAllAsync()
        {
             var menuitems=_menuItemRepository.GetTableNoTracking();
            if (!menuitems.Any())
            {
                return (null, "the menuitems is not found");
            }
            return (menuitems, "the menuItems is viewed successfully");
        }
    }
}
