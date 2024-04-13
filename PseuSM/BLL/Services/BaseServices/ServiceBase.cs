using DAL.Abstractions.Repository;
using DAL.Abstractions.UnitOfWork;
using System.Linq.Expressions;

namespace BLL.Services.BaseServices
{
    public class ServiceBase<TEntity>
        where TEntity : class
    {
        protected readonly IUnitOfWork _unitOfWork;

        public ServiceBase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        protected async Task<IRepository<TEntity, int, Expression<Func<TEntity, bool>>>> GetRepositoryAsync()
            => await _unitOfWork.GetRepositoryAsync<TEntity, int, Expression<Func<TEntity, bool>>>()
                ?? throw new InvalidOperationException("Cannot resolve UserRepository");
    }
}
