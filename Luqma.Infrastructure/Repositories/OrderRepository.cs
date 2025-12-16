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
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly LuqmaDbContext _context;

        public OrderRepository(LuqmaDbContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
            _context = context;
        }
        public async Task<string> ISCustomerVipOrNormalAsync(int customerid)
        {
            var count = _context.OrderTrackings.Count(c => c.CustomerId == customerid);
            if (count >= 3) return "VIP";
            return "Normal";
        }
        public async Task<bool> IsOrderIdExistInOrders(int orderid)
        {
            return await _context.Orders.AnyAsync(o => o.Id == orderid);
        }
        public async Task<int> GetLastOrderIdAsync(int cashierid)
        {
            var orderid = await _context.Orders.Where(o => o.CashierId == cashierid).MaxAsync(o => o.Id);
            return orderid;
        }
        public async Task<List<Order>> getOrdersForChefAsync()
        {
           return await _context.Orders.Where(o => o.Status.Equals("ReadyToPrepare")).ToListAsync();
        }
        public async Task<List<Order>> getOrdersForDeliveryAsync()
        {
            return await _context.Orders.Where(o => o.Status.Equals("Prepared") && o.Type.Equals("out of resturent")).ToListAsync();
        }
        public async Task<Order?>getOrderForDeliveryAsync(int orderid)
        {
            return await _context.Orders.Include(o => o.PaymentsOrders).Include(o => o.Customer).ThenInclude(c => c.Addresses).FirstOrDefaultAsync(o => o.Id == orderid);
        }
    }
}
