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
        public string Password { get; set; }
    }
}
