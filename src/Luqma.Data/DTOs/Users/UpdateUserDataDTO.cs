using Luqma.Data.Enums;

namespace Luqma.Data.DTOs.Users
{
    public class UpdateUserDataDTO
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
