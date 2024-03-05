using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SMS.App.Models
{
    public class LoginRequestModel
    {
        [Required(ErrorMessage = "Please provide an email.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please provide a password.")]
        [PasswordPropertyText]
        [DataType(DataType.Password)]
        [StringLength(256, MinimumLength = 8, ErrorMessage = "Ensure that password length is between 8 and 256")]
        public string Password { get; set; }
    }
}
