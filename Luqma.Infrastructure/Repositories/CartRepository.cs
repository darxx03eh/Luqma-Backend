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
    public class CartRepository : GenericRepository<Cart>, ICartRepository
    {
        private readonly LuqmaDbContext _context;

        public CartRepository(LuqmaDbContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
            _context = context;
        }
        public async Task<IQueryable<Cart>> GetCartForCustomerAsync(int? customerid)
        {
           return _context.Carts.Include(c=>c.MenuItem).Where(c => c.CustomerId == customerid).AsNoTracking().AsQueryable();
        }
        public async Task<Cart?>GetByItemIdAndCustomerIdAsync(int itemId,int CustomerId)
        {
           return _context.Carts.FirstOrDefault(c => c.ItemId == itemId && c.CustomerId == CustomerId);


        }
        public async Task<bool> CheckQuantity(int itemid)
        {
            var customerid = int.Parse(ExtractUserIdFromToken());
           var cart= _context.Carts.FirstOrDefault(c => c.ItemId == itemid && c.CustomerId == customerid);
            if (cart.Quantity == 1) return false;
            return true;
        }
        public async Task ClearCartAsync(int? customerid)
        {
          var cartsforcustomer=  await _context.Carts.Where(c => c.CustomerId == customerid).ToListAsync();
            _context.Carts.RemoveRange(cartsforcustomer);
            await _context.SaveChangesAsync();
        }
    }
}
