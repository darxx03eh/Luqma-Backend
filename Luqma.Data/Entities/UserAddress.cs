using Luqma.Data.Entities.Identity;

namespace Luqma.Data.Entities
{
    public class UserAddress
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Street { get; set; }
        public virtual LuqmaUser? User { get; set; }
    }
}
