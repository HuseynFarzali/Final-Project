using SMS.App.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SMS.App.Models
{
    public class RegisterRequestModel
    {
        [Required(ErrorMessage = "Please provide an email.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please provide a password.")]
        [PasswordPropertyText]
        [StringLength(256, MinimumLength = 8, ErrorMessage = "Ensure that password length is between 8 and 256.")]
        public string Password { get; set; }
        public UserType UserType { get; set; } = UserType.None;
    }
}
