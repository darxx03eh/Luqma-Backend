using Luqma.Data.Enums;

namespace Luqma.Data.Entities
{
    public enum Status
    {
        Active=1,
        InActive=0
    }
    public class MenuItem
    {
        public int Id { get; set; }
        public string Item { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        public double? Discount { get; set; }
        public double Price { get; set; }
        public bool IsVegetarian { get; set; }
        public double TotalStars { get; set; }
        public Status Status { get; set; } = Status.Active;
        public virtual ICollection<Feedback>? Feedbacks { get; set; } = new HashSet<Feedback>();
        public virtual ICollection<MenuContains>? MenuContains { get; set; } = new HashSet<MenuContains>();
        public virtual ICollection<CategoryItem>? CategoryItems { get; set; } = new HashSet<CategoryItem>();
        public virtual ICollection<OrderItem>? OrderItems { get; set; } = new HashSet<OrderItem>();
        public virtual ICollection<Prediction>? Predictions { get; set; } = new HashSet<Prediction>();
        public virtual ICollection<WasteReport> WasteReports { get; set; } = new HashSet<WasteReport>();
        public virtual ICollection<Cart> Carts { get; set; } = new HashSet<Cart>();
    }
}
