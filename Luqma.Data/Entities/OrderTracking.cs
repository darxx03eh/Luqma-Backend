namespace Luqma.Data.Entities
{
    public class OrderTracking
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer? Customer { get; set; }
        public virtual Order? Order { get; set; }
    }
}
