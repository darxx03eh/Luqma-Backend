namespace Luqma.Data.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ItemId { get; set; }
        public double Quantity { get; set; }
        public virtual Order? Order { get; set; }
        public virtual MenuItem? MenuItem { get; set; }
    }
}
