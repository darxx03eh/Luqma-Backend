namespace Luqma.Data.Response.Users
{
    public class ViewUsersResponse : GetUsersForFinanceResponse
    {
        public string ImageUrl { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public string Status { get; set; }
        public string LastLogin { get; set; }
        public decimal Salary { get; set; }
    }
}