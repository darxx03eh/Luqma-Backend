namespace Luqma.Data.Entities
{
    public class CustomerAddress
    {
        public int CustomerId { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Street { get; set; }
        public virtual Customer? Customer { get; set; }
    }
}
