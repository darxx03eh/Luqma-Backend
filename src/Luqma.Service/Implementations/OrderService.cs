using Luqma.Data.Entities;
using Luqma.Data.Enums;
using Luqma.Data.Response.Customers;
using Luqma.Data.Response.Order;
using Luqma.Infrastructure.IRepositories;
using Luqma.Infrastructure.Repositories;
using Luqma.Service.Interfaces;
using Stripe;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio.Types;

namespace Luqma.Service.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IWhatsAppService _whatsAppService;
        private readonly IWeatherService _weatherService;
        private readonly IOrderRepository _orderRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IPaymentOrderRepository _paymentOrderRepository;
        private readonly IDeliveriesRepository _deliveriesRepository;
        private readonly IEmailService _emailService;

        public OrderService(ICustomerRepository customerRepository, IWhatsAppService whatsAppService,
               IWeatherService weatherService, IOrderRepository orderRepository, ICartRepository cartRepository,
               IOrderItemRepository orderItemRepository,IPaymentOrderRepository paymentOrderRepository,IDeliveriesRepository deliveriesRepository)
        {
            _customerRepository = customerRepository;
            _whatsAppService = whatsAppService;
            _weatherService = weatherService;
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
            _orderItemRepository = orderItemRepository;
            _paymentOrderRepository = paymentOrderRepository;
            _deliveriesRepository = deliveriesRepository;
        }

        public async Task<(int? id, string)> AddOrderAsync(string? Note)
        {
            try
            {
                var customerid = int.Parse(_customerRepository.ExtractUserIdFromToken());
                var cust = await _customerRepository.GetByIdAsync(customerid);

                var today = DateTime.UtcNow;
                var isweekend = (today.DayOfWeek.Equals(DayOfWeek.Friday) || today.DayOfWeek.Equals(DayOfWeek.Saturday));
                var customertype = await _orderRepository.ISCustomerVipOrNormalAsync(customerid);
                var weatherState = await _weatherService.GetWeatherConditionAsync("Tulkarm");
                if (weatherState.Equals("Unknown")) return (null, "the weather is unknown");
                var holidays = new List<DateTime>()
    {
        new DateTime(today.Year, 1, 1),//راس السنة
        new DateTime(today.Year, 5, 1),//عيد العمال
        new DateTime(today.Year, 3, 30),//عيد الفطر
        new DateTime(today.Year,6,6)// عيد الاضحى
    };
                var isholiday = holidays.Any(h => h.Date.Date == today.Date);
                var order = new Order()
                {

                    CustomerId = customerid,
                    Type = "out of resturent",
                    DayOfWeek = DateTime.UtcNow.ToString("dddd", new CultureInfo("en-US")),
                    IsHoliday = isholiday,
                    IsWeekend = isweekend,
                    CustomerType = customertype,
                    PromitionApplied = customertype.Equals("VIP"),
                    WeatherConditions = weatherState,
                    Note = Note

                };
                var carts = await _cartRepository.GetCartForCustomerAsync(customerid);
                if (!carts.Any()) return (null, "the cart is empty");
                var totalprice = carts.Sum((c => c.Quantity * Math.Round(c.MenuItem.Price - (Math.Round(c.MenuItem.Discount,2) * c.MenuItem.Price))));
                order.TotalPrice = totalprice;

                await _orderRepository.AddAsync(order);

                await _whatsAppService.SendOrderIdForCustomerAsync(cust.PhoneNumber, order.Id);

                return (order.Id, "the order is added successfully");
            }


            catch (Exception ex)
            {
                return (null, ex.InnerException?.Message ?? ex.Message);
            }

        }
        public async Task<string> PlaceOrderAsync()
        {
            try
            {
                var cashierid = int.Parse(_orderRepository.ExtractUserIdFromToken());
                var today = DateTime.UtcNow;
                var isweekend = (today.DayOfWeek.Equals(DayOfWeek.Friday) || today.DayOfWeek.Equals(DayOfWeek.Saturday));
                var weatherState = await _weatherService.GetWeatherConditionAsync("Tulkarm");
                if (weatherState.Equals("Unknown")) return "the weather is unknown";
                var holidays = new List<DateTime>()
    {
        new DateTime(today.Year, 1, 1),//راس السنة
        new DateTime(today.Year, 5, 1),//عيد العمال
        new DateTime(today.Year, 3, 30),//عيد الفطر
        new DateTime(today.Year,6,6)// عيد الاضحى
    };
                var isholiday = holidays.Any(h => h.Date.Date == today.Date);
                var order = new Order()
                {
                   CashierId=cashierid,
                    Type = "Inside the restaurant",
                    DayOfWeek = DateTime.UtcNow.ToString("dddd", new CultureInfo("en-US")),
                    IsHoliday = isholiday,
                    IsWeekend = isweekend,
                    WeatherConditions = weatherState,
                    CustomerType= "Normal",
                    TotalPrice=0
                    
                };
                await _orderRepository.AddAsync(order);
                return "the order is added by Cashier successfully";
            }
            catch (Exception ex)
            {
                return ex.InnerException?.Message ?? ex.Message;
            }

        }
        public async Task<string> UpdateOnOrderTotalPriceAsync(string? Note)
        {
            var cashierid = int.Parse(_orderRepository.ExtractUserIdFromToken());
             var orderid=await _orderRepository.GetLastOrderIdAsync(cashierid);
            var order = await _orderRepository.GetByIdAsync(orderid);
            if(Note!=null) order.Note = Note;
            var orderitems = await _orderItemRepository.getOrderItemsByOrderIdAsync(orderid);
            var totalprice = orderitems.Sum(oi => oi.TotalPrice);
            order.TotalPrice = totalprice;
            await _orderRepository.SaveChangesAsync();
            return "the order is submitted";
        }
        public async Task<string> CancelOrderAsync(int orderid)
        {
            try
            {
                var order = await _orderRepository.GetByIdAsync(orderid);
                int count = await _orderRepository.DeleteAsync(order);
                if (count > 0) return "the order is canceled";
                return "the order is not canceled";
            }
            catch (Exception ex)
            {
                return ex.InnerException?.Message ?? ex.Message;
            }
        }
        public async Task<string> CancelOrderByCashierAsync()
        {
            try
            {
                var cashierid = int.Parse(_orderRepository.ExtractUserIdFromToken());
                var orderid = await _orderRepository.GetLastOrderIdAsync(cashierid);
                var order = await _orderRepository.GetByIdAsync(orderid);
                int count = await _orderRepository.DeleteAsync(order);
                if (count > 0) return "the order is canceled";
                return "the order is not canceled";
            }
            catch(Exception ex)
            {
                return ex.InnerException?.Message ?? ex.Message;
            }
        }
        public async Task<string> UpdateOrderByCashierAsync(int id,string? Note)
        {
           var order= await _orderRepository.GetByIdAsync(id);
            order.Note = Note;
            order.Status = "ReadyToPrepare";
            await _orderRepository.SaveChangesAsync();
            return "the order is updated by Cashier successfully";
        }
        public async Task<(List<Order>,string)> getOrdersForChefAsync()
        {
           var orders= await _orderRepository.getOrdersForChefAsync();
            return (orders, "the orders is fetched successfully");

        }
        public async Task<string> ChangeStatusByChefAsync(int orderid)
        {
            var order = await _orderRepository.GetByIdAsync(orderid);
            order.Status = "Prepared";
            await _orderRepository.UpdateAsync(order);
            return "the order status is updated successfully";
        }
        public async Task<(List<Order>,string)> getOrdersForDeliveryAsync()
        {
          var orders=  await _orderRepository.getOrdersForDeliveryAsync();
            return (orders, "the orders is fetched successfully");

        }
        public async Task<(ViewOrderDetailsByDeliveryResponse,string)> getOrderDetailsForDeliveryAsync(int orderid)
        {
            var order=await _orderRepository.getOrderForDeliveryAsync(orderid);
            var addresses=order.Customer.Addresses.Select(a => new CustomerAddressResponse
            {
                State = a.State,
                City = a.City,
                Street = a.Street

            });
            var customerAddressResponse=addresses.Last();
            var Payment = await _paymentOrderRepository.getOrderPaymentbyIdAsync(orderid);
            var orderDetails = new ViewOrderDetailsByDeliveryResponse
            {
                FirstName = order.Customer.FirstName,
                LastName = order.Customer.LastName,
                PhoneNumber = order.Customer.PhoneNumber,
                Address = customerAddressResponse,
                Status = Payment.Payment.Status


            };
            return (orderDetails, "the order details is fetched successfully");

        }
        public async Task<string> ChangeStatusToOutByDeliveryAsync( int orderid)
        {
            var Deliveryid = int.Parse(_orderRepository.ExtractUserIdFromToken());
            var order= await _orderRepository.GetByIdAsync(orderid);
            order.Status = "OutForDelivery";
            await _orderRepository.UpdateAsync(order);
            var deliveries = new Deliveries
            {
                DeliveryId = Deliveryid,
                OrderId = orderid
            };
            await _deliveriesRepository.AddAsync(deliveries);
            return "the order is updated successfully";
        }
        public async Task<(List<ViewOrderResponse>,string)> getOutOrdersForDeliveryAsync()
        {
            var Deliveryid = int.Parse(_orderRepository.ExtractUserIdFromToken());
            var deliveries = await _deliveriesRepository.getOrdersForDeliveryAsync(Deliveryid);
            var ordersRe = new List<ViewOrderResponse>();
            foreach(var o in deliveries)
            {
                var order = new ViewOrderResponse()
                {
                    Id = o.Order.Id,
                    Type = o.Order.Type,
                    TotalPrice = o.Order.TotalPrice,
                    Status = o.Order.Status,
                    Date = o.Order.Date.ToString("MM/dd/yyyy hh:mm:ss tt"),
                    Note = o.Order.Note
                };
                ordersRe.Add(order);
            }
            return (ordersRe, "the orders is fetched successfully");
        }
        public async Task<string> ChangeStatusToDeliveredByDeliveryAsync(int orderid)
        {
          var order= await  _orderRepository.GetByIdAsync(orderid);
            order.Status = "Delivered";
           await  _orderRepository.UpdateAsync(order);
          var orderPayment=  await _paymentOrderRepository.getOrderPaymentbyIdAsync(orderid);
            if (orderPayment.Payment.Status.Equals("UnPaid"))
            {
                orderPayment.Payment.Status = "paid";
               await  _paymentOrderRepository.SaveChangesAsync();
            }
            return "the order is updated successfully";
        }
    }
}

       
        

    

