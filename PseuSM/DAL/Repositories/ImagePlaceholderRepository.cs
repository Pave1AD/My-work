using DAL.Entities;
using DAL.Repositories.BaseRepositories;

namespace DAL.Repositories
{
    public class ImagePlaceholderRepository : BaseRepository<ImagePlaceholder>
    {
        public ImagePlaceholderRepository(MainContext context) : base(context)
        {
        }
    }
}
