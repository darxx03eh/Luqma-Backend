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
    public class OrderTrackingRepository : GenericRepository<OrderTracking>, IOrderTrackingRepository
    {
        private readonly LuqmaDbContext _context;

        public OrderTrackingRepository(LuqmaDbContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
            _context = context;
        }
        public async Task<string> ISCustomerVipOrNormalAsync(int customerid)
        {
           var count= _context.OrderTrackings.Count(c => c.CustomerId == customerid);
            if (count >=3) return "VIP";
            return "Normal";
        }
    }
}
