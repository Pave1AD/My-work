using BLL.Entities;
using System.Security.Claims;

namespace BLL.Abstractions.Services
{
    public interface IJwtService
    {
        Task<string> GenerateAccessTokenAsync(User user);
        Task<string> GenerateRefreshTokenAsync(User user);
        Task<ClaimsPrincipal> ValidateTokenAsync(string token, bool justRead);
    }
}
