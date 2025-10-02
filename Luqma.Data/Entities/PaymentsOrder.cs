namespace Luqma.Data.Entities
{
    public class PaymentsOrder
    {
        public int Id { get; set; }
        public int PaymentId { get; set; }
        public int OrderId { get; set; }
        public virtual Payment? Payment { get; set; }
        public virtual Order? Order { get; set; }
    }
}
