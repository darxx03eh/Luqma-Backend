using Luqma.Data.Enums;
using Luqma.Data.Helpers;
using Microsoft.AspNetCore.Identity;

namespace Luqma.Data.Entities.Identity
{
    public class LuqmaUser : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public DateTime BirthDate { get; set; }
        private string? code;
        public string? Code
        {
            get => code is null ? null : EncryptionHelper.Decrypt(code);
            set => code = value is null ? null : EncryptionHelper.Encrypt(value);
        }
        private string? forgetPasswordToken;
        public string? ForgetPasswordToken
        {
            get => forgetPasswordToken is null ? null : EncryptionHelper.Decrypt(forgetPasswordToken);
            set => forgetPasswordToken = value is null ? null : EncryptionHelper.Encrypt(value);
        }
        private string? phoneNumberCode;
        public string? PhoneNumberCode
        {
            get => phoneNumberCode is null ? null:EncryptionHelper.Decrypt(phoneNumberCode);
            set => phoneNumberCode = value is null ? null : EncryptionHelper.Encrypt(value); 
        }
        public DateTime? PhoneNumberCodeExpiryDate { get; set; }
        public DateTime? CodeExpiryDate { get; set; }
        public decimal Salary { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsActive { get; set; } = true;
        public int? ManagerId { get; set; }
        public virtual ICollection<UserRefreshToken>? UserRefreshTokens { get; set; } = new HashSet<UserRefreshToken>();
        public virtual LuqmaUser? Manager { get; set; }
        public virtual ICollection<LuqmaUser>? Subordinates { get; set; } = new HashSet<LuqmaUser>();
        public virtual ICollection<Salary>? UserSalaries { get; set; } = new HashSet<Salary>();
        public virtual ICollection<Salary>? FinanceSalaries { get; set; } = new HashSet<Salary>();
        public virtual ICollection<Deduction>? UserDeductions { get; set; } = new HashSet<Deduction>();
        public virtual ICollection<Deduction>? FinanceDeductions { get; set; } = new HashSet<Deduction>();
        public virtual ICollection<UserAddress>? Addresses { get; set; } = new HashSet<UserAddress>();
        public virtual ICollection<Bill>? Bills { get; set; } = new HashSet<Bill>();
        public virtual ICollection<KitchenRequirments>? KitchenRequirments { get; set; } = new HashSet<KitchenRequirments>();
    }
}
