using Luqma.Data.Entities;
using Luqma.Data.Enums;
using Luqma.Infrastructure.IRepositories;
using Luqma.Service.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
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

        public OrderService(ICustomerRepository customerRepository,IWhatsAppService whatsAppService)
        {
            _customerRepository = customerRepository;
            _whatsAppService = whatsAppService;
        }

       
        }

    }

