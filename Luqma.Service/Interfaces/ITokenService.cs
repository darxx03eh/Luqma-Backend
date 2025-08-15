using Luqma.Data.Entities.Identity;
using Luqma.Data.Response.Authentications;
using System.IdentityModel.Tokens.Jwt;

namespace Luqma.Service.Interfaces
{
    public interface ITokenService
    {
        public Task<SignInResponse> GenerateAccessTokenAsync(LuqmaUser user);
        public Task<JwtSecurityToken> ReadJwtTokenAsync(string token);
        public Task<(JwtSecurityToken, string)> GenerateJwtTokenAsync(LuqmaUser user);
        public Task<string> GenerateRandomRefreshToken();
    }
}
