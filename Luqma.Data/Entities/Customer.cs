using Luqma.Data.Enums;

namespace Luqma.Data.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
        public Gender gender { get; set; }
        public virtual ICollection<Order>? Orders { get; set; } = new HashSet<Order>();
    }
}
