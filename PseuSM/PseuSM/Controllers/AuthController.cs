using Adapters.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using AdapterRegisterUser = Adapters.Entities.RegisterUser;
using AdapterLoginUser = Adapters.Entities.LoginUser;
using AdaptersJwtToken = Adapters.Entities.JwtToken;
using Core;
using PseuSM.Entities;
using PseuSM.Models;
using Microsoft.AspNetCore.Http;

namespace PseuSM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserAuthAdapter _userAdapter;
        private readonly IMapper _mapper;

        public AuthController(IUserAuthAdapter userAdapter, IMapper mapper)
        {
            _userAdapter = userAdapter;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromForm]RegisterUserModel registerUser)
        {
            RegisterUser user = _mapper.Map<RegisterUser>(registerUser);

            var test = _mapper.Map<RegisterUser, AdapterRegisterUser>(user);

            AdaptersJwtToken jwtToken = await _userAdapter.RegisterUserAsync(_mapper.Map<RegisterUser, AdapterRegisterUser>(user));

            SetRefreshTokenInHttpOnlyCookie(jwtToken.RefreshToken);

            return Ok(_mapper.Map<AdaptersJwtToken, JwtResponse>(jwtToken));
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(LoginUser loginUser)
        {
            AdaptersJwtToken jwtToken = await _userAdapter.LoginUserAsync(_mapper.Map<LoginUser, AdapterLoginUser>(loginUser));

            SetRefreshTokenInHttpOnlyCookie(jwtToken.RefreshToken);

            return Ok(_mapper.Map<AdaptersJwtToken, JwtResponse>(jwtToken));
        }

        [HttpGet("refresh")]
        public async Task<IActionResult> RefreshAccessToken()
        {
            string refreshToken = Request.Cookies.FirstOrDefault(cookie => cookie.Key == CookiesConstants.RefreshToken).Value;

            AdaptersJwtToken jwtToken = await _userAdapter.RefreshTokensAsync(refreshToken);

            SetRefreshTokenInHttpOnlyCookie(jwtToken.RefreshToken);

            return Ok(_mapper.Map<AdaptersJwtToken, JwtResponse>(jwtToken));
        }

        private void SetRefreshTokenInHttpOnlyCookie(string refreshToken)
        {
            Response.Cookies.Append(CookiesConstants.RefreshToken, refreshToken, new CookieOptions
            {
                HttpOnly = true
            });
        }
    }
}
