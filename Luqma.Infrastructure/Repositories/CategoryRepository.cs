using Luqma.Data.Entities;
using Luqma.Infrastructure.Data;
using Luqma.Infrastructure.IRepositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Infrastructure.Repositories
{
   public  class CategoryRepository:GenericRepository<Category>,ICategoryRepository
    {
        private readonly LuqmaDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CategoryRepository(LuqmaDbContext context,IHttpContextAccessor httpContextAccessor):base(context,httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
       
        public async Task<bool> IsTitleExistAsync(string Title)
        {
           var category=  _context.Categories.FirstOrDefault(cat => cat.Title.Equals(Title));
            if (category is null) return false;
            return true;
        }
    }
}
