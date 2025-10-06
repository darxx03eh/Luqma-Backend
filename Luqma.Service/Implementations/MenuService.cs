using Luqma.Data.Entities;
using Luqma.Infrastructure.IRepositories;
using Luqma.Service.Interfaces;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Service.Implementations
{
   public class MenuService : IMenuService
    {
        private readonly IMenuRepository _menuRepository;

        public MenuService(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }
        public async Task<string> AddMenuAsync(Menu menu)
        {
            if(menu is null)
            {
                return "the menu is null";
            }
          await _menuRepository.AddAsync(menu);
            return "the menu is added successfully";

        }
        public async Task<(IQueryable<Menu>?, string)> GetAllMenusAsync()
        {
           var menus= _menuRepository.GetTableNoTracking();
            if (!menus.Any())
            {
                return (null, "the menus is not found");
            }
            return (menus, "the menus is viewed successfully");


        }
        public async Task<string> DeleteMenuAsync(int id)
        {
            var menu = await _menuRepository.GetByIdAsync(id);
            if (menu is null) return "the menu id is not found";
           
          var count= await  _menuRepository.DeleteAsync(menu);
            return count > 0 ? "the menu is deleted successfully" : "the menu is not deleted";
        }
        public async Task<string> updateMenuAsync(int id,string title,string description)
        {

            var menu = await _menuRepository.GetByIdAsync(id);
            if(menu is null)
            {
                return "the menu id is not found";
            }
            menu.Title = title;
            menu.Description = description;
           int count= await _menuRepository.UpdateAsync(menu);

            return count > 0 ? "the menu is updated successfully" : "the menu is not updated";
        }

      
    }
}
