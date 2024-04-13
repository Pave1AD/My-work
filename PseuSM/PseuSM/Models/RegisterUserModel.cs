namespace PseuSM.Models
{
    public class RegisterUserModel
    {
        public string Name { get; set; } = string.Empty;
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public IFormFile Avatar { get; set; } = null!;
    }
}
