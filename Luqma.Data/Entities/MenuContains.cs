namespace Luqma.Data.Entities
{
    public class MenuContains
    {
        public int Id { get; set; }
        public int MenuId { get; set; }
        public int ItemId { get; set; }
        public virtual Menu? Menu { get; set; }
        public virtual MenuItem? MenuItem { get; set; }
    }
}
