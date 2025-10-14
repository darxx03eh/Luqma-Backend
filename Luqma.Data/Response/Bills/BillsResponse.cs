namespace Luqma.Data.Response.Bills
{
    public class BillsResponse
    {
        public int Id { get; set; }
        public string FinanceName { get; set; }
        public string BillType {  get; set; }
        public double Amount { get; set; }
        public string DueDate { get; set; }
        public string Status { get; set; }
    }
}
