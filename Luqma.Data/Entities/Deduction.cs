using Luqma.Data.Entities.Identity;

namespace Luqma.Data.Entities
{
    public class Deduction
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int FinanceId { get; set; }
        public double DeductionRate { get; set; }
        public DateTime DeductionDate { get; set; } = DateTime.UtcNow;
        public virtual LuqmaUser? User { get; set; }
        public virtual LuqmaUser? Finance { get;set; }
    }
}
