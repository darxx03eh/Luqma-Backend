namespace Luqma.Data.Response.KitchenRequirements
{
    public class GetKitchenRequirementsInfoResponse : GetKitchenRequirementsResponse
    {
        public IList<Items> Items { get; set; }
    }
    public class Items
    {
        public int ItemId { get; set; }
        public string Item { get; set; }
        public string? ImageUrl { get; set; }
        public string Unit { get; set; }
        public RequirementInfo RequirementInfo { get; set; }
    }
    public class RequirementInfo
    {
        public double Price { get; set; }
        public double Quantity { get; set; }
        public double Discount { get; set; }
    }
}
