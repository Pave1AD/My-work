using DAL.Entities.Cloudinary;
using DAL.Repositories.BaseRepositories;

namespace DAL.Repositories
{
    public class CloudinaryImageRepository : BaseRepository<CloudinaryImage>
    {
        public CloudinaryImageRepository(MainContext context) : base(context)
        {
        }
    }
}
