using Adapters.Abstractions;
using Adapters.Entities;
using AutoMapper;
using BLL.Abstractions.Services;
using BLLUser = BLL.Entities.User;

namespace Adapters
{
    public class UserAdapter : IUserAdapter
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserAdapter(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            BLLUser user = await _userService.GetUserByIdAsync(id);
            return _mapper.Map<User>(user);
        } 
    }
}
