using Luqma.Data.Entities.Identity;

namespace Luqma.Data.Entities
{
    public class KitchenRequirements
    {
        public int Id { get; set; }
        public int ChefId { get; set; }
        public double TotalPrice { get; set; }
        public string Status { get; set; }
        public string? Note { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public virtual LuqmaUser? Chef { get; set; }
        public virtual ICollection<RequirementItems>? RequirementItems { get; set; } = new HashSet<RequirementItems>();

    }
}
