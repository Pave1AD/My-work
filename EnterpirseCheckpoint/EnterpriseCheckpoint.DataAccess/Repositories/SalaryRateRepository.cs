using EnterpriseCheckpoint.Models.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EnterpriseCheckpoint.DataAccess.Repositories
{
    public class SalaryRateRepository : BaseRepository<SalaryRate>
    {
        public SalaryRateRepository(DbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<SalaryRate>> ReadEntitiesByPredicate(Expression<Func<SalaryRate, bool>> predicate, IEnumerable<KeyValuePair<Expression<Func<SalaryRate, object>>, bool>>? orderBy = null,
            CancellationToken cancellationToken = default)
        {
            var query = _dbSet
                .Where(predicate);

            if (orderBy is not null)
            {
                foreach (var order in orderBy)
                {
                    query = order.Value ? query.OrderBy(order.Key) : query.OrderByDescending(order.Key);
                }
            }

            return await query.ToListAsync(cancellationToken);
        }

    }
}
