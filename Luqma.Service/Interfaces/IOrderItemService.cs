using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luqma.Service.Interfaces
{
    public interface IOrderItemService
    {
        public Task<string> AddItemToOrderAsync(int ItemId, int quantity);
        public  Task<string> DeleteItemFromOrderAsync(int ItemId);
    }
}
