namespace Luqma.Data.Entities
{
    public class RequirmentItems
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int RequirmentId { get; set; }
        public double Quantity { get; set; }
        public double Discount { get; set; }
        public double Price { get; set; }
        public virtual KitchenItems? KitchenItems { get; set; }
        public virtual KitchenRequirments? KitchenRequirments { get; set; }
    }
}
