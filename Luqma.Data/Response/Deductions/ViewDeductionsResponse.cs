namespace Luqma.Data.Response.Deductions
{
    public class ViewDeductionsResponse
    {
        public int Id { get; set; }
        public User User { get; set; }
        public string FinanceName { get; set; }
        public string DeductionDate { get; set; }
        public double DeductionRate { get; set; }
    }
    public class User
    {
        public int Id { get; set; }
        public string? ImageUrl { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
