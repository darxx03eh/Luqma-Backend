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
    public class PaymentOrderRepository : GenericRepository<PaymentsOrder>, IPaymentOrderRepository
    {
        private readonly LuqmaDbContext _context;

        public PaymentOrderRepository(LuqmaDbContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
            _context = context;
        }
        public async Task<PaymentsOrder?>getOrderPaymentbyIdAsync(int orderid)
        {
            return await _context.PaymentsOrders.Include(p=>p.Payment).FirstOrDefaultAsync(op => op.OrderId == orderid);
        }
    }
}
