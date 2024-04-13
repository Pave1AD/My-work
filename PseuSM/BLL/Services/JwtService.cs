using AutoMapper;
using BLL.Abstractions.Services;
using BLL.Entities;
using BLL.Services.BaseServices;
using Core;
using DAL.Abstractions.UnitOfWork;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DALUser = DAL.Entities.User;

namespace BLL.Services
{
    public class JwtService : ServiceBase<DALUser>, IJwtService
    {
        private readonly JwtProperties _jwtProperties;

        public JwtService(
            IUnitOfWork unitOfWork,
            IOptions<JwtProperties> jwtPropertiesOptions) : base(unitOfWork)
        {
            _jwtProperties = jwtPropertiesOptions.Value ?? throw new ArgumentNullException(nameof(jwtPropertiesOptions));
        }

        public Task<string> GenerateAccessTokenAsync(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(UserClaimsConstants.Id, user.Id.ToString()),
                new Claim(UserClaimsConstants.Name, user.Name!),
            };

            return GetJwtTokenAsync(claims);
        }

        public Task<string> GenerateRefreshTokenAsync(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(UserClaimsConstants.Id, user.Id.ToString()),
                new Claim(UserClaimsConstants.Name, user.Name!),
            };

            return GetJwtTokenAsync(claims);
        }

        public Task<ClaimsPrincipal> ValidateTokenAsync(string token, bool justRead)
        {
            var handler = new JwtSecurityTokenHandler();
            ClaimsPrincipal result = handler.ValidateToken(token, new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtProperties.SecretKey)),
                ValidateIssuerSigningKey = !justRead,
            }, out _);

            return Task.FromResult(result);
        }

        private Task<string> GetJwtTokenAsync(List<Claim> claims)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtProperties.SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtProperties.Issuer,
                audience: _jwtProperties.Audience,
                claims: claims,
                signingCredentials: credentials
                );

            var handler = new JwtSecurityTokenHandler();

            return Task.FromResult(handler.WriteToken(token));
        }
    }
}
