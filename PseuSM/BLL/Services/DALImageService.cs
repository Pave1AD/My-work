using BLL.Abstractions.Services;
using BLL.Services.BaseServices;
using DAL.Abstractions.Repository;
using DAL.Abstractions.UnitOfWork;
using DAL.Entities;
using System.Linq.Expressions;

namespace BLL.Services
{
    public class DALImageService : ServiceBase<Image>, IDALImageService
    {
        public DALImageService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<Image?> GetDALImageByIdAsync(int id)
        {
            IRepository<Image, int, Expression<Func<Image, bool>>> imageRepository = await GetRepositoryAsync();

            return await imageRepository.GetEntityByIdAsync(id);
        }
    }
}
