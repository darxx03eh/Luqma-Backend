using Luqma.Data.Entities;
using Luqma.Data.Response.Feedbacks;
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
    public class DeliveriesRepository : GenericRepository<Deliveries>, IDeliveriesRepository
    {
        private readonly LuqmaDbContext _context;

        public DeliveriesRepository(LuqmaDbContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
            _context = context;
        }
        public async Task<List<Deliveries>> getOrdersForDeliveryAsync(int deliveryid)
        {
            return await _context.Deliveries.Where(d => d.DeliveryId == deliveryid).ToListAsync();
        }
    }
}
