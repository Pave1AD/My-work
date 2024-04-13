using Adapters.Entities;

namespace Adapters.Abstractions
{
    public interface IUserAdapter
    {
        Task<User> GetUserByIdAsync(int id);
    }
}
