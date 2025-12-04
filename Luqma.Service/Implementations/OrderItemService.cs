using Luqma.Data.Entities;
using Luqma.Infrastructure.IRepositories;
using Luqma.Service.Interfaces;
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

        public OrderItemService(IOrderRepository orderRepository,IOrderItemRepository orderItemRepository)
        {
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
        }
        public async Task<string> AddItemToOrderAsync(int ItemId,int quantity)
        {
            var cashierid = int.Parse(_orderItemRepository.ExtractUserIdFromToken());
           var orderid= await _orderRepository.GetLastOrderIdAsync(cashierid);
            var orderitem = new OrderItem()
            {
                OrderId = orderid,
                ItemId = ItemId,
                Quantity=quantity
            };
           await  _orderItemRepository.AddAsync(orderitem);
            return "the item is added to order successfully"; 
        }
    }
}
