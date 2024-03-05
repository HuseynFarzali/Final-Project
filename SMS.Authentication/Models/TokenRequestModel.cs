using AutoMapper.Configuration.Annotations;
using SMS.Tools.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Authentication.Models
{
    public class TokenRequestModel
    {
        public string Email { get; set; }
        public UserType UserType { get; set; }
        public bool IsAdmin { get; set; }
    }
}
