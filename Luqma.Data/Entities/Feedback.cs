namespace Luqma.Data.Entities
{
    public class Feedback
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int ItemId { get; set; }
        public string? Content { get; set; }
        public int Stars { get; set; }
        public virtual Customer? Customer { get; set; }
        public virtual MenuItem? MenuItem { get; set; }
    }
}
