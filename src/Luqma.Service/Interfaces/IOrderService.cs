using Luqma.Data.Entities;
using Luqma.Data.Response.Order;
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
        public Task<string> UpdateOrderByCashierAsync(int id, string? Note);
        public  Task<(List<Order>, string)> getOrdersForChefAsync();
        public Task<string> ChangeStatusByChefAsync(int orderid);
        public Task<(List<Order>, string)> getOrdersForDeliveryAsync();
        public Task<(ViewOrderDetailsByDeliveryResponse, string)> getOrderDetailsForDeliveryAsync(int orderid);
        public Task<string> ChangeStatusToOutByDeliveryAsync(int orderid);
        public Task<string> ChangeStatusToDeliveredByDeliveryAsync(int orderid);
        public Task<(List<ViewOrderResponse>, string)> getOutOrdersForDeliveryAsync();
    }
}
