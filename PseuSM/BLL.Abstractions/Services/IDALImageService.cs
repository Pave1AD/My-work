using DAL.Entities;

namespace BLL.Abstractions.Services
{
    public interface IDALImageService
    {
        public Task<Image?> GetDALImageByIdAsync(int id); 
    }
}
