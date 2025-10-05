using Luqma.Data.Enums;

namespace Luqma.Data.Response.Users
{
    public class ProfileResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? ImageUrl { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
        public string Gender { get; set; }
        public string BirthDate { get; set; }
        public string JoinDate { get; set; }
    }
}
