using Core.Enums;

namespace BLL.Entities
{
    public class ImagePlaceholder
    {
        public int Id { get; set; }
        public ImageTypes Type { get; set; }
        public Image Image { get; set; } = null!;
    }
}
