using DAL.Entities.BaseEntities;

namespace DAL.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Login { get; set; }
        public string? Password { get; set; }
        public string? RefreshToken { get; set; }

        public IEnumerable<Image> Images { get; set; } = new List<Image>();
    }
}
