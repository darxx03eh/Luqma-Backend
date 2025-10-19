namespace Luqma.Data.Response.KitchenRequirments
{
    public class GetKitchenRequirmentsInfoResponse : GetKitchenRequirmentsResponse
    {
        public IList<Items> Items { get; set; }
    }
    public class Items
    {
        public int ItemId { get; set; }
        public string Item { get; set; }
        public string? ImageUrl { get; set; }
        public string Unit { get; set; }
        public RequirmentInfo RequirmentInfo { get; set; }
    }
    public class RequirmentInfo
    {
        public double Price { get; set; }
        public double Quantity { get; set; }
        public double Discount { get; set; }
    }
}
