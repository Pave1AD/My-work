namespace DAL.Abstractions.Repository
{
    public interface IRepository<TEntity, TId, TPredicate>
        where TEntity : class
    {
        Task<TEntity?> GetEntityByIdAsync(TId id);
        Task<IEnumerable<TEntity>> GetEntitiesByPredicateAsync(TPredicate predicate);
        Task CreateEntityAsync(TEntity entity);
        Task UpdateEntityAsync(TEntity entity);
        Task DeleteEntityAsync(TId id);
    }
}
