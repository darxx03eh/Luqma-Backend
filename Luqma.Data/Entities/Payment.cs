namespace Luqma.Data.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public double? Amount { get; set; }
        public string Status { get; set; }
        public string PaymentMethod { get; set; }
        public string Currency { get; set; }
        public string? Note { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public virtual ICollection<PaymentsOrder> PaymentsOrders { get; set; } = new HashSet<PaymentsOrder>();
    }
}
