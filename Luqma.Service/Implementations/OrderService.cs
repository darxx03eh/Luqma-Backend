using Luqma.Data.Entities;
using Luqma.Data.Enums;
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
        private readonly IEmailService _emailService;

        public OrderService(ICustomerRepository customerRepository, IWhatsAppService whatsAppService,
               IWeatherService weatherService, IOrderRepository orderRepository, ICartRepository cartRepository,
               IOrderItemRepository orderItemRepository)
        {
            _customerRepository = customerRepository;
            _whatsAppService = whatsAppService;
            _weatherService = weatherService;
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
            _orderItemRepository = orderItemRepository;
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
                var totalprice = carts.Sum((c => c.Quantity * (c.MenuItem.Price - (c.MenuItem.Discount * c.MenuItem.Price))));
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
       /* public async Task<string> UpdateOnOrderTotalPrice(string? Note)
        {
            var cashierid = int.Parse(_orderRepository.ExtractUserIdFromToken());
             var orderid=await _orderRepository.GetLastOrderIdAsync(cashierid);

            var order = await _orderRepository.GetByIdAsync(orderid);
            if(Note!=null) order.Note = Note;
            var orderitems = await _orderItemRepository.getOrderItemsByOrderIdAsync(orderid);
           var totalprice= orderitems.Sum((oi => oi.Quantity * (oi.MenuItem.Price - (oi.MenuItem.Discount * oi.MenuItem.Price))));





        }*/
    }
}

       
        

    

