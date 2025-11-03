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
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMenuRepository _menuRepository;
        private readonly ICategoryItemRepository _categoryItemRepository;
        private readonly IMenuContainsRepository _menuContainsRepository;

        public MenuItemService(IMenuItemRepository menuItemRepository 
            ,ICloudinaryService cloudinaryService,
            ICategoryRepository categoryRepository,
            IMenuRepository menuRepository
           )
        {
            _menuItemRepository = menuItemRepository;
            _cloudinaryService = cloudinaryService;
            _categoryRepository = categoryRepository;
            _menuRepository = menuRepository;
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
        public async Task<string> DeleteMenuItemAsync(int id)
        {

            var item = await _menuItemRepository.GetByIdAsync(id);
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
        public async Task<string> UpdateMenuItemAsync(int id,string item,string description,double? discount,double price,bool isvegetarian,IFormFile? file)
        {

            var menuitem = await _menuItemRepository.GetByIdAsync(id);
            if (menuitem is null) return "the item Id is not found";
            menuitem.Item = item;
            menuitem.Description = description;
            menuitem.Price = price;
            menuitem.Discount = discount;
            menuitem.IsVegetarian = isvegetarian;
     

           
            if (file == null && menuitem.ImageUrl!=null)
            {
                var result = await _cloudinaryService.DeleteFileAsync(menuitem.ImageUrl);
                if (result.Equals("FailedToDeleteImageFromCloudinary") || result.Equals("AnErrorOccurredWhileDeletingFromCloudinary"))
                    return "An Error while delete photo from  Cloudinary ";
                menuitem.ImageUrl = null;

                
            }

                
            if ( file!=null && menuitem.ImageUrl != null)
            {
               var result= await _cloudinaryService.DeleteFileAsync(menuitem.ImageUrl);
                if (result.Equals("FailedToDeleteImageFromCloudinary") || result.Equals("AnErrorOccurredWhileDeletingFromCloudinary"))
                    return "An Error while delete photo from  Cloudinary ";

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var fileurl = await _cloudinaryService.UploadFileAsync(file.OpenReadStream(), "Luqma/MenuItems", fileName);
                menuitem.ImageUrl = fileurl;
                
            }
            if(file!=null && menuitem.ImageUrl==null)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var fileurl = await _cloudinaryService.UploadFileAsync(file.OpenReadStream(), "Luqma/MenuItems", fileName);
                menuitem.ImageUrl = fileurl;
            }
         


                var count = await _menuItemRepository.UpdateAsync(menuitem);
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
        public async Task<(MenuItem,string)>GetByIdAsync(int id)
        {
           var item= await  _menuItemRepository.GetByIdAsync(id);
           
            return (item,"the item is fetched successfully");
        }
        public async Task<string> ToggleStatusAsync(int id)
        {
         var item=  await _menuItemRepository.GetByIdAsync(id);
            if (item.Status == Status.InActive)
            {
                item.Status = Status.Active;
            }
            else {
                item.Status = Status.InActive;
            }
            await _menuItemRepository.SaveChangesAsync();
            return "the status of item is toggled";
                
        }
    }
}
