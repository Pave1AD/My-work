using DAL.Repositories.BaseRepositories;
using DAL.Entities;

namespace DAL.Repositories
{
    public class ImageRepository : BaseRepository<Image>
    {
        public ImageRepository(MainContext context) : base(context)
        {
        }
    }
}
