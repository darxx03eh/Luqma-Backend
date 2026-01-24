namespace Luqma.Data.Entities
{
    public class Menu
    {
        public int Id { get; set; }
        public string Title { get;set; }
        public string? Description { get; set; }
        public virtual ICollection<MenuContains> MenuContains { get; set; } = new HashSet<MenuContains>();
    }
}
