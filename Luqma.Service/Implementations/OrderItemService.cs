using Luqma.Data.Entities;
using Luqma.Infrastructure.IRepositories;
using Luqma.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Service.Implementations
{
   public class OrderItemService:IOrderItemService

    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IMenuItemRepository _menuItemRepository;

        public OrderItemService(IOrderRepository orderRepository,IOrderItemRepository orderItemRepository,IMenuItemRepository menuItemRepository)
        {
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _menuItemRepository = menuItemRepository;
        }
        public async Task<string> AddItemToOrderAsync(int ItemId,int quantity)
        {
            var cashierid = int.Parse(_orderItemRepository.ExtractUserIdFromToken());
           var orderid= await _orderRepository.GetLastOrderIdAsync(cashierid);
            var item = await _menuItemRepository.GetByIdAsync(ItemId);
            var orderitem = new OrderItem()
            {
                OrderId = orderid,
                ItemId = ItemId,
                Quantity = quantity,
                TotalPrice = Math.Round((item.Price - (item.Price * item.Discount)) * quantity)
            };
           await  _orderItemRepository.AddAsync(orderitem);
            return "the item is added to order successfully"; 
        }
        public async Task<string> DeleteItemFromOrderAsync(int ItemId)
        {
            var cashierid = int.Parse(_orderItemRepository.ExtractUserIdFromToken());
            var orderid = await _orderRepository.GetLastOrderIdAsync(cashierid);
             var orderItem=await _orderItemRepository.getOrderItemAsync(orderid, ItemId);
             await  _orderItemRepository.DeleteAsync(orderItem);
            return "the item is deleted from order successfully";
        }
        public async Task<(List<Order>,string)> GetOrdersAsync()
        {
            var orderitems = await _orderItemRepository.getOrderItemsAsync();
            var orders = new List<Order>();
            foreach(var oi in orderitems)
            {
                var order = new Order()
                {
                    Id = oi.OrderId,
                    Note = oi.Order.Note,
                    TotalPrice = oi.Order.TotalPrice,
                    Type = oi.Order.Type,
                    Status = oi.Order.Status,
                    Date = oi.Order.Date
                };
                orders.Add(order);
            }
            return (orders, "the orders is fetched successfully");
        }
        public async Task<(List<OrderItem>,string)> GetOrderDetailsAsync(int orderid)
        {
            var orderitems = await _orderItemRepository.getOrderItemsByOrderIdAsync(orderid);
            return (orderitems, "the order details is fetched successfully");
            
        }
        public async Task<string> UpdateQuantityForItemByCashierAsync(int orderid,int itemid,double quantity)
        {
          var orderitem=  await _orderItemRepository.getOrderItemAsync(orderid, itemid);
            orderitem.Quantity = quantity;
            orderitem.TotalPrice = Math.Round((orderitem.MenuItem.Price - (orderitem.MenuItem.Price * orderitem.MenuItem.Discount)) * quantity);
           await _orderItemRepository.SaveChangesAsync();
            var order = await _orderRepository.GetByIdAsync(orderid);
           var orderitems= await _orderItemRepository.getOrderItemsByOrderIdAsync(orderid);
            var TotalPrice=orderitems.Sum(oi => oi.TotalPrice);
            order.TotalPrice = TotalPrice;
           await  _orderRepository.SaveChangesAsync();
            return "the quantity is updated by cashier successfully";


        }
    }
}
