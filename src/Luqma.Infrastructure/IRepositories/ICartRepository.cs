using Luqma.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Infrastructure.IRepositories
{
    public interface ICartRepository : IGenericRepository<Cart>
    {

        public Task<IQueryable<Cart>> GetCartForCustomerAsync(int? customerid);
        public Task<Cart?> GetByItemIdAndCustomerIdAsync(int itemId, int CustomerId);
        public Task<bool> CheckQuantity(int itemid);
        public Task ClearCartAsync(int? customerid);
    }
}
