namespace Luqma.Data.Response.KitchenRequirements
{
    public class GetKitchenRequirementsResponse
    {
        public int Id { get; set; }
        public string ChefName { get; set; }
        public double TotalPrice { get; set; }
        public string Status { get; set; }
        public string? Note { get; set; }
        public string Date { get; set; }
    }
}
