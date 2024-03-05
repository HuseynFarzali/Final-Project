using SMS.App.Enums;

namespace SMS.App.Models
{
    public class AppUser
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public bool IsAdmin { get; set; } = false;
        public UserType UserType { get; set; } = UserType.None;
    }
}
