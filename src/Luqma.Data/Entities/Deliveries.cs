using Luqma.Data.Entities.Identity;

namespace Luqma.Data.Entities
{
    public class Deliveries
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int DeliveryId { get; set; }
        public virtual LuqmaUser? Delivery { get; set; }
        public virtual Order? Order { get; set; }
    }
}
