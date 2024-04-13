using Adapters.Abstractions;
using AutoMapper;
using Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PseuSM.Entities;

namespace PseuSM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserAdapter _userAdapter;

        public UsersController(IMapper mapper, IUserAdapter userAdapter)
        {
            _mapper = mapper;
            _userAdapter = userAdapter;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("current")]
        public async Task<IActionResult> GetCurrentUserInformation()
        {
            int id = int.Parse(User.Claims.FirstOrDefault(claim => claim.Type == UserClaimsConstants.Id)?.Value ?? throw new ArgumentException("Unable to retrieve id"));

            return Ok(_mapper.Map<User>(await _userAdapter.GetUserByIdAsync(id)));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserInformationById(int id)
        {
            return Ok(_mapper.Map<User>(await _userAdapter.GetUserByIdAsync(id)));
        }
    }
}
