namespace Luqma.Data.Entities
{
    public class WasteReport
    {
        public int Id { get; set; }
        public int? ItemId { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime CapturedAta { get; set; } = DateTime.UtcNow;
        public double WasteQuantity { get; set; }
        public string? Reason { get; set; }
        public virtual MenuItem? MenuItem { get; set; }
    }
}