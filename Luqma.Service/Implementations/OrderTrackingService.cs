using Luqma.Infrastructure.IRepositories;
using Luqma.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Service.Implementations
{
    public class OrderTrackingService :IOrderTrackingService
    {
        private readonly IOrderTrackingRepository _orderTrackingRepository;

        public OrderTrackingService(IOrderTrackingRepository orderTrackingRepository)
        {
            _orderTrackingRepository = orderTrackingRepository;
        }
      
    }
}
