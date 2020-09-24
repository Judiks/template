using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using THD.Domain.Entities;
using THD.Domain.Helpers;
using THD.Domain.Models.AccountModels.Response;
using THD.Infrastructure.Options;

namespace THD.Infrastructure.Helpers
{
    public class JwtHelper : IJwtHelper
    {
        private IOptions<AuthTokenProviderOption> _tokenOptions;
        public JwtHelper(IOptions<AuthTokenProviderOption> tokenOptions)
        {
            _tokenOptions = tokenOptions;
        }
        public JwtAuthentificationResponse GenerateToken(ApplicationUser user, IEnumerable<string> userRoles)
        {
            DateTime now = DateTime.Now;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions?.Value?.JwtKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var accessTokenClaims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName.ToString()),
            };
            accessTokenClaims.AddRange(userRoles.Select(item =>
            {
                return new Claim(ClaimTypes.Role, item);
            }));
            var refreshTokenClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier,user.Id)
            };
            refreshTokenClaims.AddRange(userRoles.Select(item =>
            {
                return new Claim(ClaimTypes.Role, item);
            }));
            DateTime accessTokenExpires = now.Add(_tokenOptions.Value.AccessTokenExpiration);
            DateTime refreshTokenExpires = now.Add(_tokenOptions.Value.RefreshTokenExpiration);
            JwtSecurityToken accessToken = GenerateToken(null, accessTokenExpires, accessTokenClaims, credentials);
            JwtSecurityToken refreshToken = GenerateToken(null, refreshTokenExpires, refreshTokenClaims, credentials);
            var encodedAccessToken = new JwtSecurityTokenHandler().WriteToken(accessToken);
            var encodedRefreshToken = new JwtSecurityTokenHandler().WriteToken(refreshToken);
            return new JwtAuthentificationResponse()
            {
                Id = user.Id,
                Roles = userRoles.ToList(),
                FullName = user.FirstName + " " + user.LastName,
                AccessToken = encodedAccessToken,
                RefreshToken = encodedRefreshToken,
            };
        }
        private static JwtSecurityToken GenerateToken(string audience, DateTime expiration, List<Claim> claims, SigningCredentials credentials)
        {
            return new JwtSecurityToken(
              audience: audience,
              claims: claims,
              expires: expiration,
              signingCredentials: credentials);
        }
    }
}
