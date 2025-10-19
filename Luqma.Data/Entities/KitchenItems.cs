namespace Luqma.Data.Entities
{
    public class KitchenItems
    {
        public int Id { get; set; }
        public string Item { get;set; }
        public double Price { get; set; }
        public string? ImageUrl { get; set; }
        public string? Status { get; set; }
        public string? Note { get; set; }
        public string Unit { get; set; }
        public double Quantity { get; set; }
        public virtual ICollection<RequirmentItems>? RequirmentItems { get; set; } = new HashSet<RequirmentItems>();
    }
}
