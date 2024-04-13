using Adapters.Entities;

namespace Adapters.Abstractions
{
    public interface IUserAuthAdapter
    {
        Task<JwtToken> RegisterUserAsync(RegisterUser registerUser);
        Task<JwtToken> LoginUserAsync(LoginUser loginUser);
        Task<JwtToken> RefreshTokensAsync(string refreshToken);
    }
}
