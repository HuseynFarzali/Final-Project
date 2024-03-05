using SMS.Tools.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Authentication.Models
{
    public class RegisterRequestModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; } = false;
        public UserType? UserType { get; set; } = Tools.Enums.UserType.None;
    }
}
