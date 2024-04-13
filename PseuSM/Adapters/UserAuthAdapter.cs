using Adapters.Abstractions;
using Adapters.Entities;
using AutoMapper;
using BLL.Abstractions.Services;
using Core;
using System.Security.Claims;
using Tools.Abstractions;
using BLLUser = BLL.Entities.User;
using BLLImage = BLL.Entities.Image;
using Core.Enums;

namespace Adapters
{
    public class UserAuthAdapter : IUserAuthAdapter
    {
        private readonly IUserService _userService;
        private readonly IImageService _imageService;
        private readonly IJwtService _jwtService;
        private readonly IHasher _hasher;
        private readonly IMapper _mapper;

        public UserAuthAdapter(
            IUserService userService,
            IImageService imageService,
            IJwtService jwtService,
            IHasher hasher,
            IMapper mapper)
        {
            _userService = userService;
            _imageService = imageService;
            _jwtService = jwtService;
            _hasher = hasher;
            _mapper = mapper;
        }

        public async Task<JwtToken> RegisterUserAsync(RegisterUser registerUser)
        {
            registerUser.Password = _hasher.Hash(registerUser.Password);

            BLLUser user = _mapper.Map<RegisterUser, BLLUser>(registerUser);

            user = await _userService.CreateUserAsync(user);

            if (registerUser.AvatarStream == null)
            {
                await _imageService.CreatePlaceholderImageAsync(ImageTypes.Avatar, user);
            }
            else
            {
                await _imageService.CreateImageFromStreamAsync(registerUser.AvatarStream, ImageTypes.Avatar, user);
            }

            return await FormJwtTokenAsync(user);
        }

        public async Task<JwtToken> LoginUserAsync(LoginUser loginUser)
        {
            loginUser.Password = _hasher.Hash(loginUser.Password);

            BLLUser user = await _userService.GetUserByCredentialsAsync(loginUser.Login, loginUser.Password);

            return await FormJwtTokenAsync(user);
        }

        public async Task<JwtToken> RefreshTokensAsync(string refreshToken)
        {
            ClaimsPrincipal accessTokenPrincipal = await _jwtService.ValidateTokenAsync(refreshToken, true);

            BLLUser user = await _userService.GetUserByIdAsync(int.Parse(accessTokenPrincipal.Claims.FirstOrDefault(claim => claim.Type == UserClaimsConstants.Id)?.Value 
                ?? throw new ArgumentException("Unable to resolve id claim")));

            if (user.RefreshToken != refreshToken)
            {
                throw new ArgumentException("Invalid refresh token");
            }

            return await FormJwtTokenAsync(user);
        }

        private async Task<JwtToken> FormJwtTokenAsync(BLLUser user)
        {
            string refreshToken = await _jwtService.GenerateRefreshTokenAsync(user);
            string accessToken = await _jwtService.GenerateAccessTokenAsync(user);

            await AttachRefreshTokenToUser(refreshToken, user);

            var result = new JwtToken()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
            };

            return result;
        }

        private Task AttachRefreshTokenToUser(string refreshToken, BLLUser user)
        {
            user.RefreshToken = refreshToken;
            return _userService.UpdateUserAsync(user);
        }
    }
}
