using Luqma.Data.Entities.Identity;
using Luqma.Data.Response.Authentications;

namespace Luqma.Service.Interfaces
{
    public interface IAuthenticationService
    {
        public Task<string> SignUpAsync(LuqmaUser user, string password, string role);
        public Task<(SignInResponse?, string)> SignInAsync(string username, string password);
        public Task<string> ConfirmationEmailAsync(string email, string token);
        public Task<string> SendConfirmationEmailAsync(string email);
        public Task<string> SendForgetPasswordAsync(string email);
        public Task<(string, string?)> ForgetPasswordConfirmationAsync(string email, string code);
        public Task<string> ResetPasswordAsync(string email, string password, string token);
        public Task<(SignInResponse, string)> GenerateRefreshTokenAsync(string accessToken, string refreshToken);
        public Task<string> RevokeRefreshTokenAsync(string refreshToken);
        public Task<string> ValidateAccessToken(string token);
        public Task<string> SendConfirmationCodeThenAddAsync(string phoneNumber);
        public Task<string> ConfirmationPhoneNumberAsync(string code);
    }
}
