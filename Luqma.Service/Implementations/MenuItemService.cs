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
            ,ICloudinaryService cloudinaryService,
            ICategoryItemRepository categoryItemRepository,
            IMenuContainsRepository menuContainsRepository)
        {
            _menuItemRepository = menuItemRepository;
            _cloudinaryService = cloudinaryService;
            _categoryItemRepository = categoryItemRepository;
            _menuContainsRepository = menuContainsRepository;
        }
        public async Task<string> AddMenuItemAsync(MenuItem menuItem,IFormFile file, ICollection<int> CategoryId,ICollection<int>MenuId)
        {
            var categoryitems = new HashSet<CategoryItem>();
            var menucontains = new HashSet<MenuContains>();
            if (file != null)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var imageurl = await _cloudinaryService.UploadFileAsync(file.OpenReadStream(), "Menuitems", fileName);
                menuItem.ImageUrl = imageurl;
            }
          var menuitem= await  _menuItemRepository.AddAsync(menuItem);
            foreach(var cat in CategoryId)
            {
                categoryitems.Add(new CategoryItem()
                {
                    ItemId = menuitem.Id,
                    CategoryId = cat
                });
            }
            menuitem.CategoryItems = categoryitems;
            foreach (var men in MenuId)
            {
                menucontains.Add(new MenuContains()
                {
                    ItemId = menuitem.Id,
                    MenuId = men
                });
            }
            menuItem.MenuContains = menucontains;
           await  _categoryItemRepository.AddRangeAsync(categoryitems);
            await _menuContainsRepository.AddRangeAsync(menucontains);
            return "the menuitem is added successfully";

            



            


          
            

        }
    }
}
