using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.DAL.Data.Entities.Concrete
{
    public class ChatUser
    {
        public int ChatId { get; set; }
        public Chat? Chat { get; set; }

        public int AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}
