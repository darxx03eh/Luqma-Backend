namespace Luqma.Data.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public virtual ICollection<CategoryItem> CategoryItems { get; set; } = new HashSet<CategoryItem>();
    }
}
