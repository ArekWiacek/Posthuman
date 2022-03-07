namespace Posthuman.Core.Models.DTO
{
    public class RegisterUserDTO
    {
        public RegisterUserDTO()
        {
            Name = "";
            Email = "";
            Password = "";
            ConfirmPassword = "";
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
