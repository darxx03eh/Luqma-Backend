namespace Luqma.Data.Entities
{
    public class Feedback
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int ItemId { get; set; }
        public string? Content { get; set; }
        public double Stars { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public virtual Customer? Customer { get; set; }
        public virtual MenuItem? MenuItem { get; set; }
    }
}
