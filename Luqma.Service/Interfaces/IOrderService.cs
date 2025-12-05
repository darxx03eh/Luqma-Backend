using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Service.Interfaces
{
   public  interface IOrderService
    {
        public Task<(int? id, string)> AddOrderAsync(string? Note);
        public  Task<string> PlaceOrderAsync();
        public Task<string> UpdateOnOrderTotalPriceAsync(string? Note);
        public Task<string> CancelOrderAsync(int orderid);
        public Task<string> CancelOrderByCashierAsync();
    }
}
