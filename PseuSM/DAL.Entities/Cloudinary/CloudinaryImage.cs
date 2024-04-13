using DAL.Entities.BaseEntities;

namespace DAL.Entities.Cloudinary
{
    public class CloudinaryImage : BaseEntity
    {
        public string PublicId { get; set; } = string.Empty;

        public ICollection<Image> Images { get; set; } = new List<Image>();
    }
}
