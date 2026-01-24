using Luqma.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Service.Interfaces
{
    public interface ICartService
    {

        public  Task<string> AddToCartAsync(int itemid);
        public Task<(IQueryable<Cart>?, string)> GetCartForCustomerAsync();
        public  Task<string> IncreaseQuantityAsync(int itemId);
        public Task<string> DecreaseQuantityAsync(int itemId);
        public Task<string> DeleteItemFromCartForCustomerAsync(int itemid);
    }
}
