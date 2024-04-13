using DAL.Abstractions.Repository;
using DAL.Entities;
using DAL.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DAL.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(MainContext context) : base(context)
        {

        }

        public override Task<User?> GetEntityByIdAsync(int id)
        {
            return _context.Users.Include(user => user.Images).FirstOrDefaultAsync(user => user.Id == id);
        }

        public override async Task<IEnumerable<User>> GetEntitiesByPredicateAsync(Expression<Func<User, bool>> predicate)
        {
            return await _context.Users.Include(user => user.Images).Where(predicate).ToListAsync();
        }
    }
}
