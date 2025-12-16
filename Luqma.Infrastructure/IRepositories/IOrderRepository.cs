using Luqma.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Infrastructure.IRepositories
{
   public  interface IOrderRepository : IGenericRepository<Order>
    {

        public  Task<string> ISCustomerVipOrNormalAsync(int customerid);
        public Task<bool> IsOrderIdExistInOrders(int orderid);
        public Task<int> GetLastOrderIdAsync(int cashierid);
        public Task<List<Order>> getOrdersForChefAsync();
        public Task<List<Order>> getOrdersForDeliveryAsync();
        public Task<Order?> getOrderForDeliveryAsync(int orderid);
    }
}
