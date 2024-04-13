using BLL.Entities;
using System.Linq.Expressions;

namespace BLL.Abstractions.Services
{
    public interface IUserService
    {
        Task<User> GetUserByCredentialsAsync(string login, string password);
        Task<User> GetUserByIdAsync(int id);
        Task<User> CreateUserAsync(User user);
        Task<User> UpdateUserAsync(User user);
        Task DeleteUserAsync(User user);
    }
}
