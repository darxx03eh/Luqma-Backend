using Luqma.Data.Enums;
using Luqma.Data.Helpers;

namespace Luqma.Data.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        private string? code;
        public string? Code
        {
            get => code is null ? null : EncryptionHelper.Decrypt(code);
            set => code = value is null ? null : EncryptionHelper.Encrypt(value);
        }
        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
        public Gender gender { get; set; }
        public virtual ICollection<Order>? Orders { get; set; } = new HashSet<Order>();
        public virtual ICollection<CustomerAddress>? Addresses { get; set; } = new HashSet<CustomerAddress>();
        public virtual ICollection<OrderTracking>? OrderTrackings { get; set; } = new HashSet<OrderTracking>();
        public virtual ICollection<Feedback>? Feedbacks { get; set; } = new HashSet<Feedback>();
    }
}
