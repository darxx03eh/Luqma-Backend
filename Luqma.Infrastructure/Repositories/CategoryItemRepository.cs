using Luqma.Data.Entities;
using Luqma.Infrastructure.Data;
using Luqma.Infrastructure.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Infrastructure.Repositories
{
    public class CategoryItemRepository : GenericRepository<CategoryItem>, ICategoryItemRepository
    {
        private readonly LuqmaDbContext _context;

        public CategoryItemRepository(LuqmaDbContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
            _context = context;
        }

        public async Task<IQueryable<CategoryItem>> GetItemsByCategoryIdAsync( int id)
        {
             return _context.CategoryItems.Include(ci => ci.MenuItem).Where(ci => ci.CategoryId == id).AsNoTracking().AsQueryable();
        }
    }
}
