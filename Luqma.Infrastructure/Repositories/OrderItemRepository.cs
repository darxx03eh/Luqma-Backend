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
    public class OrderItemRepository : GenericRepository<OrderItem>,IOrderItemRepository
    {
        private readonly LuqmaDbContext _context;

        public OrderItemRepository(LuqmaDbContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
            _context = context;
        }
       public async Task<List<OrderItem>> getOrderItemsByOrderIdAsync(int id)
        {
            return await  _context.OrderItems.Include(oi => oi.MenuItem).Where(oi => oi.OrderId == id).ToListAsync();
        }
    }
}
