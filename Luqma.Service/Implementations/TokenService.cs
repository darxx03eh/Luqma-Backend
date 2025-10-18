using Luqma.Data.Entities;
using Luqma.Data.Entities.Identity;
using Luqma.Data.Helpers;
using Luqma.Data.Response.Authentications;
using Luqma.Data.Response.Deductions;
using Luqma.Infrastructure.IRepositories;
using Luqma.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Luqma.Service.Implementations
{
    public class TokenService(UserManager<LuqmaUser> userManager
                            , JwtSettings jwtSettings
                            , IRefreshTokenRepository refreshTokenRepository,ICustomerRepository customerRepository) : ITokenService
    {
        private readonly UserManager<LuqmaUser> userManager = userManager;
        private readonly JwtSettings jwtSettings = jwtSettings;
        private readonly IRefreshTokenRepository refreshTokenRepository = refreshTokenRepository;
        private readonly ICustomerRepository _customerRepository = customerRepository;

        public async Task<SignInResponse> GenerateAccessTokenAsync(LuqmaUser user)
        {
            var (jwtToken, accessToken) = await GenerateJwtTokenAsync(user);
            var refreshToken = await GenerateRefreshToken(user.UserName);
            var userRefreshToken = new UserRefreshToken()
            {
                AddedTime = DateTime.UtcNow,
                ExpirydDate = DateTime.UtcNow.AddDays(jwtSettings.RefreshTokenExpireDate),
                IsUsed = true,
                IsRevoked = false,
                JwtID = jwtToken.Id,
                Token = accessToken,
                UserId = user.Id,
                RefreshToken = refreshToken.Token
            };
            await refreshTokenRepository.AddAsync(userRefreshToken);
            return new SignInResponse()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }
        public async Task<(JwtSecurityToken, string)> GenerateJwtTokenAsync(LuqmaUser user)
        {
            var userClaims = await GenerateUserClaimsAsync(user);
            var jwtToken = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                claims: userClaims,
                expires: DateTime.UtcNow.AddDays(jwtSettings.AccessTokenExpireDate),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.SecretKey))
                    , SecurityAlgorithms.HmacSha256Signature));
            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return (jwtToken, accessToken);
        }
        private async Task<List<Claim>> GenerateUserClaimsAsync(LuqmaUser user)
        {
            var roles = await userManager.GetRolesAsync(user);
            var claims = new List<Claim>()
            {
                new Claim(nameof(UserClaimModel.UserName), user.UserName),
                new Claim(nameof(UserClaimModel.Email), user.Email),
                new Claim(nameof(UserClaimModel.FirstName), user.FirstName),
                new Claim(nameof(UserClaimModel.LastName), user.LastName),
                new Claim(nameof(UserClaimModel.Id), user.Id.ToString())
            };
            claims.AddRange(roles.Select(role => new Claim(nameof(UserClaimModel.Role), role)));
            return claims;
        }

        public async Task<string> GenerateJwtTokenForCustomerAsync(Customer customer)
        {
            
            var userClaims = await GenerateUserClaimsForCustomerAsync(customer);
            var jwtToken = new JwtSecurityToken(
               issuer: jwtSettings.Issuer,
               audience: jwtSettings.Audience,
               claims: userClaims,
               expires: DateTime.UtcNow.AddDays(jwtSettings.CustomerTokenExpireDate),
               signingCredentials: new SigningCredentials(
                   new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.SecretKey))
                   , SecurityAlgorithms.HmacSha256Signature));
            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return  accessToken;
        }
        private async Task<List<Claim>> GenerateUserClaimsForCustomerAsync(Customer customer)
        {
           
            var claims = new List<Claim>()
            {
               
                new Claim(nameof(UserClaimModel.Id), customer.Id.ToString()),
                new Claim(nameof(UserClaimModel.PhoneNumber), customer.PhoneNumber.ToString())

                
            };
           
            return claims;
        }

        private async Task<RefreshToken> GenerateRefreshToken(String username)
            => new RefreshToken()
            {
                UserName = username,
                ExpireAt = DateTime.UtcNow.AddDays(jwtSettings.RefreshTokenExpireDate),
                Token = await GenerateRandomRefreshToken()
            };
        public async Task<string> GenerateRandomRefreshToken()
        {
            var random = new byte[32];
            var randomGenerate = RandomNumberGenerator.Create();
            randomGenerate.GetBytes(random);
            return Convert.ToBase64String(random);
        }

        public async Task<JwtSecurityToken> ReadJwtTokenAsync(String token)
        {
            if (String.IsNullOrEmpty(token))
                throw new ArgumentNullException(nameof(token));
            var handler = new JwtSecurityTokenHandler();
            var response = handler.ReadJwtToken(token);
            return response;
        }
    }
}
