﻿namespace Adapters.Entities
{
    public class RegisterUser
    {
        public string Name { get; set; } = string.Empty;
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public Stream? AvatarStream { get; set; }
    }
}
