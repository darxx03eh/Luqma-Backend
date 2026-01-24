using Luqma.Data.Entities;
using Luqma.Data.Response.OrdersTracking;
using Luqma.Infrastructure.IRepositories;
using Luqma.Service.Interfaces;

namespace Luqma.Service.Implementations
{
    public class OrderTrackingService : IOrderTrackingService
    {
        private readonly IUnitOfWork unitOfWork;

        public OrderTrackingService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<(string, OrderTrackingResponse?)> OrderTrackingAsync(int id)
        {
            var customerId = unitOfWork.CustomerRepository.ExtractUserIdFromToken();
            if (string.IsNullOrWhiteSpace(customerId))
                return ("CustomerNotFound", null);

            var customer = await unitOfWork.CustomerRepository.GetByIdAsync(Convert.ToInt32(customerId));
            if (customer is null)
                return ("CustomerNotFound", null);

            var order = await unitOfWork.OrderRepository.GetByIdAsync(id);
            if (order is null)
                return ("OrderNotFound", null);

            var isOrderBelongsToCustomer = order.CustomerId.Equals(customer.Id);
            if (!isOrderBelongsToCustomer)
                return ("ThisOrderDoNotBelongToYou", null);

            var delivery = order.Deliveries.Where(d => d.OrderId.Equals(order.Id)).FirstOrDefault();
            IList<Items> items = new List<Items>();
            foreach (var item in order.OrderItems)
                items.Add(new Items()
                {
                    Id = item.Id,
                    ItemName = item.MenuItem.Item,
                    ImageUrl = item.MenuItem.ImageUrl,
                    Quantity = Convert.ToInt32(item.Quantity),
                    TotalPrice = Convert.ToDouble(item.TotalPrice)
                });
            var tracking = new OrderTracking() { OrderId = order.Id, CustomerId = customer.Id };
            var trackingResult = await unitOfWork.OrderTrackingRepository.AddAsync(tracking);
            if (trackingResult is null)
                return ("OrderTrackingFailed", null);
            var orderTracking = new OrderTrackingResponse()
            {
                OrderTrackingId = trackingResult.Id,
                OrderId = order.Id,
                Status = order.Status,
                TotalItems = order.OrderItems.Count(oi => oi.OrderId.Equals(order.Id)),
                TotalPrice = Convert.ToDouble(order.TotalPrice),
                Delivery = new Delivery()
                {
                    Id = delivery is not null ? delivery.Delivery.Id : null,
                    Name = delivery is not null ? $"{delivery.Delivery.FirstName} {delivery.Delivery.LastName}" : null,
                    PhoneNumber = delivery is not null ? delivery.Delivery.PhoneNumber : null
                },
                Items = items
            };
            return ("TheOrderWasFoundAndTrackingTheOrder", orderTracking);
        }
    }
}