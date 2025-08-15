using Luqma.Core.Bases;
using Luqma.Data.Enums;
using MediatR;

namespace Luqma.Core.Features.Authentications.Commands.Models
{
    public class SignUpCommand : IRequest<ApiResponse>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public Gender Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public decimal Salary { get; set; }
        public string Role { get; set; }
    }
}
