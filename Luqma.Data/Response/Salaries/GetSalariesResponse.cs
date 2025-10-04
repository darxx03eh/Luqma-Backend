using Luqma.Data.Response.Deductions;

namespace Luqma.Data.Response.Salaries
{
    public class GetSalariesResponse
    {
        public int Id { get; set; }
        public User User { get; set; }
        public string FinanceName { get; set; }
        public string Status { get; set; }
        public double SalarayAdmount { get; set; }

    }
}
