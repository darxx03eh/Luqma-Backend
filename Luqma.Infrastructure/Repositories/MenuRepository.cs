using Luqma.Data.Entities;
using Luqma.Infrastructure.Data;
using Luqma.Infrastructure.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Infrastructure.Repositories
{
    public class MenuRepository : GenericRepository<Menu>, IMenuRepository
    {
        private readonly LuqmaDbContext _context;

        public MenuRepository(LuqmaDbContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
            _context = context;
        }
        public async Task<bool> IsIdInMenuAsync(ICollection<int>MenuId)
        {
            foreach(var i in MenuId)
            {
               var menu= _context.Menus.FirstOrDefault(m => m.Id == i);
                if(menu is null)
                {
                    return false;
                }
            }
            return true;

        }
        public async Task<bool> IsIdExistInMenuAsync(int id)
        {
           var menu= _context.Menus.FirstOrDefault(m => m.Id == id);
            if (menu is null) return false;
            return true;
        }
    }
}
