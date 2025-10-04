using Luqma.Data.Entities.Identity;

namespace Luqma.Data.Entities
{
    public class Salary
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int FinanceId { get; set; }
        public string Status { get; set; }
        public DateTime SalaryDate { get; set; } = DateTime.UtcNow;
        public double SalaryAmount { get; set; }
        public virtual LuqmaUser? User { get; set; }
        public virtual LuqmaUser? Finance { get; set; }
    }
}
