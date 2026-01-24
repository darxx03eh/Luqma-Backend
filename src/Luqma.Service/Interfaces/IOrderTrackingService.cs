using Luqma.Data.Response.OrdersTracking;

namespace Luqma.Service.Interfaces
{
    public interface IOrderTrackingService
    {
        public Task<(string, OrderTrackingResponse?)> OrderTrackingAsync(int id);
    }
}