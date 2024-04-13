namespace BLL.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
        public string? RefreshToken { get; set; }

        public IEnumerable<Image> Images { get; set; } = new List<Image>();
    }
}
