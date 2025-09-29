namespace Luqma.Data.Entities
{
    public class MenuItem
    {
        public int Id { get; set; }
        public string Item { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        public double? Discount { get; set; }
        public double Price { get; set; }
        public bool IsVegetarian { get; set; }
        public virtual ICollection<Feedback>? Feedbacks { get; set; } = new HashSet<Feedback>();
        public virtual ICollection<MenuContains> MenuContains { get; set; } = new HashSet<MenuContains>();
    }
}
