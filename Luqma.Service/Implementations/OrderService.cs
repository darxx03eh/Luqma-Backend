using Luqma.Data.Entities;
using Luqma.Data.Enums;
using Luqma.Infrastructure.IRepositories;
using Luqma.Service.Interfaces;
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
        private readonly IOrderTrackingRepository _orderTrackingRepository;
        private readonly IWeatherService _weatherService;

        public OrderService(ICustomerRepository customerRepository,IWhatsAppService whatsAppService,
            IOrderTrackingRepository orderTrackingRepository,IWeatherService weatherService)
        {
            _customerRepository = customerRepository;
            _whatsAppService = whatsAppService;
            _orderTrackingRepository = orderTrackingRepository;
            _weatherService = weatherService;
        }
      /*  public async Task<string> AddOrder(double TotalPrice, ICollection<int> MenuItemId,int customerid,string paymentMethod)
        {
            var today = DateTime.UtcNow;
            var isweekend = (today.DayOfWeek.Equals(DayOfWeek.Friday) || today.DayOfWeek.Equals(DayOfWeek.Saturday));
            var customertype = await _orderTrackingRepository.ISCustomerVipOrNormalAsync(customerid);
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
                TotalPrice = TotalPrice,
                CustomerId = customerid,
                Type = "out of resturent",
                DayOfWeek = DateTime.UtcNow.ToString("dddd", new CultureInfo("en-US")),
                IsHoliday = isholiday,
                IsWeekend = isweekend,
                CustomerType = customertype,
                PromitionApplied = customertype.Equals("VIP"),
                WeatherConditions = weatherState
            };

         





        }*/

       
        }

    }

