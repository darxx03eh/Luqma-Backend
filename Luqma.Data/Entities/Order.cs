using Luqma.Data.Entities.Identity;

namespace Luqma.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int CashierId { get; set; }
        public int CustomerId { get; set; }
        public double TotalPrice { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string Type { get; set; }
        public string? Note { get; set; }
        public string DayOfWeek { get; set; }
        public bool IsHoliday { get; set; }
        public string? WeatherConditions { get; set; }
        public string CustomerType { get; set; }
        public bool IsWeekend { get; set; }
        public string? EventTag { get; set; }
        public bool PromitionApplied { get; set; }
        public virtual ICollection<Deliveries>? Deliveries { get; set; } = new HashSet<Deliveries>();
        public virtual ICollection<PaymentsOrder>? PaymentsOrders { get; set; } = new HashSet<PaymentsOrder>();
        public virtual ICollection<OrderTracking>? OrderTrackings { get; set; } = new HashSet<OrderTracking>();
        public virtual ICollection<OrderItem>? OrderItems { get; set; } = new HashSet<OrderItem>();
        public virtual LuqmaUser? Cashier { get; set; }
        public virtual Customer? Customer { get; set; }
    }
}
