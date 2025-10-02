namespace Luqma.Data.Entities
{
    public class CategoryItem
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int ItemId { get; set; }
        public virtual MenuItem? MenuItem { get; set; }
        public virtual Category? Category { get; set; }
    }
}
