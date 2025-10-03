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
                var imageurl = await _cloudinaryService.UploadFileAsync(file.OpenReadStream(), "Menuitems", fileName);
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
    }
}
