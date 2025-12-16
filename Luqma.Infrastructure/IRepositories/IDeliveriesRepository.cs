using Luqma.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Infrastructure.IRepositories
{
    public interface IDeliveriesRepository : IGenericRepository<Deliveries>
    {
        public Task<List<Deliveries>> getOrdersForDeliveryAsync(int deliveryid);
    }
}
