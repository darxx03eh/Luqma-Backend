using Luqma.Data.Entities.Identity;

namespace Luqma.Data.Entities
{
    public class Bill
    {
        public int Id { get; set; }
        public int FinanceId { get; set; }
        public string BillType { get; set; }
        public string? Note { get; set; }
        public double TotalPrice { get; set; }
        public virtual LuqmaUser? Finance { get; set; }
    }
}
