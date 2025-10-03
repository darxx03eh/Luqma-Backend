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
   public  class MenuItemRepository:GenericRepository<MenuItem>,IMenuItemRepository
    {

        private readonly LuqmaDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MenuItemRepository(LuqmaDbContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }


    }
}
