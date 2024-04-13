namespace Adapters.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Login { get; set; } = string.Empty;
        public IEnumerable<Image> Images { get; set; }
    }
}
