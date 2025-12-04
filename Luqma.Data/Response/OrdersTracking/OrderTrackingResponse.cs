namespace Luqma.Data.Response.OrdersTracking
{
    public class OrderTrackingResponse
    {
        public int OrderTrackingId { get; set; }
        public int OrderId { get; set; }
        public string Status { get; set; }
        public int TotalItems { get; set; }
        public double TotalPrice { get; set; }
        public Delivery Delivery { get; set; }
        public IList<Items> Items { get; set; }
    }

    public class Delivery
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class Items
    {
        public int Id { get; set; }
        public string? ImageUrl { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }
    }
}