using SMS.DAL.Data.Entities.Abstract;
using SMS.Tools.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.DAL.Data.Entities.Concrete
{
    public class AppUser : Entity
    {
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public bool IsAdmin { get; set; } = false;
        public UserType UserType { get; set; } = UserType.None;

        public List<ChatUser>? ChatUsers { get; set; }
    }
}
