namespace Luqma.Data.Entities.Identity
{
    public class UserRefreshToken
    {
        public int ID { get; set; }
        public int UserId { get; set; }
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public string? JwtID { get; set; }
        public bool IsUsed { get; set; }
        public bool IsRevoked { get; set; }
        public DateTime AddedTime { get; set; }
        public DateTime ExpirydDate { get; set; }
        public virtual LuqmaUser? User { get; set; }
    }
}
