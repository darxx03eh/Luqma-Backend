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
   public class MenuService :IMenuService
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
    }
}
