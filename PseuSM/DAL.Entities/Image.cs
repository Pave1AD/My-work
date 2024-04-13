using Core.Enums;
using DAL.Entities.BaseEntities;

namespace DAL.Entities
{
    public class Image : BaseEntity
    {
        public string? Reference { get; set; } = string.Empty;
        public ImageTypes Type { get; set; } = ImageTypes.Regular;

        public int? UserId { get; set; }
        public User? User { get; set; } = null!;
    }
}
