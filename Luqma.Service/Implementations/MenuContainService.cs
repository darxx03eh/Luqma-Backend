using Luqma.Data.Entities;
using Luqma.Infrastructure.IRepositories;
using Luqma.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Service.Implementations
{
   public class MenuContainService: IMenuContainService
    {
        private readonly IMenuContainsRepository _menuContainsRepository;

        public MenuContainService(IMenuContainsRepository menuContainsRepository)
        {
            _menuContainsRepository = menuContainsRepository;
        }
        public async Task<(IQueryable<MenuContains>?, string)> GetItemsByMenuIdAsync(int id)
        {
          var menucontains= await  _menuContainsRepository.GetItemsByMenuIdAsync(id);
            if (!menucontains.Any())
            {
                return (null, "the menu id is not found");
            }
            return (menucontains, "the items is fetched successfully");


        }
    }
}
