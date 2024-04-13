using DAL.Abstractions.Repository;
using DAL.Entities.BaseEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace DAL.Repositories.BaseRepositories
{
    public class BaseRepository<TEntity> : IRepository<TEntity, int, Expression<Func<TEntity, bool>>>
        where TEntity : BaseEntity
    {
        protected readonly MainContext _context;

        public BaseRepository(MainContext context)
        {
            _context = context;
        }

        public virtual async Task CreateEntityAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }

        public virtual async Task DeleteEntityAsync(int id)
        {
            TEntity? entityToDelete = await GetEntityByIdAsync(id);

            if (entityToDelete == null)
            {
                return;
            }

            _context.Set<TEntity>().Remove(entityToDelete);
        }

        public virtual async Task<IEnumerable<TEntity>> GetEntitiesByPredicateAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public virtual Task<TEntity?> GetEntityByIdAsync(int id)
        {
            return _context.Set<TEntity>().FirstOrDefaultAsync(user => user.Id == id);
        }

        public virtual async Task UpdateEntityAsync(TEntity entity)
        {
            TEntity? oldEntity = await GetEntityByIdAsync(entity.Id);

            if (oldEntity == null)
            {
                await CreateEntityAsync(entity);
                return;
            }

            EntityEntry<TEntity> oldEntityEntry = _context.Set<TEntity>().Entry(oldEntity);

            oldEntityEntry.CurrentValues.SetValues(entity);
        }
    }
}
