using Posthuman.Core.Models.Entities.Authentication;

namespace Posthuman.Core.Models.Entities
{
    public class User
    {
        public User()
        {
            Email = "";
            PasswordHash = "";
            IsAdmin = false;
            RoleId = 1;
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public bool IsAdmin { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; } = default!;
        public Avatar Avatar { get; set; } = default!;
    }
}
