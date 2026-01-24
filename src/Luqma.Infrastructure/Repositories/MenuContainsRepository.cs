using Luqma.Data.Entities;
using Luqma.Infrastructure.Data;
using Luqma.Infrastructure.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Infrastructure.Repositories
{
    public class MenuContainsRepository : GenericRepository<MenuContains>, IMenuContainsRepository
    {
        private readonly LuqmaDbContext _context;

        public MenuContainsRepository(LuqmaDbContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
            _context = context;
        }
        public async Task<IQueryable<MenuContains>> GetItemsByMenuIdAsync(int id)
        {
            return _context.MenuContains.Include(mc => mc.MenuItem).Where(mc => mc.MenuId == id).AsNoTracking().AsQueryable();

        }
    }
}
