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
            return await  _context.OrderItems.Include(oi => oi.MenuItem).Where(oi => oi.OrderId == id).AsQueryable().AsNoTracking().ToListAsync();
        }
        public async Task<OrderItem?>getOrderItemAsync(int orderid,int itemid)
        {
           return await _context.OrderItems.FirstOrDefaultAsync(oi => oi.OrderId == orderid && oi.ItemId == itemid);
        }
        public async Task<List<OrderItem>> getOrderItemsAsync(int cashierid)
        {
           var orderitems= await _context.OrderItems.Include(oi => oi.Order).Where(oi=>oi.Order.Status.Equals("Pending") && oi.Order.TotalPrice!=0).ToListAsync();
            var orderItems= orderitems.DistinctBy(oi => oi.OrderId).ToList();
            orderItems.RemoveAll(oi => oi.Order.CashierId != null && oi.Order.CashierId != cashierid);
      
            return orderItems;
        }
        public async Task<bool> isOrderIdInOrderitemsAsync(int orderid)
        {
            var order=_context.OrderItems.FirstOrDefault(oi => oi.OrderId == orderid);
            if (order is null) return false;
            return true;
        }

     
    }
}
