using SMS.DAL.Data.Entities.Abstract;
using SMS.DAL.Data.Entities.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.DAL.Data.Entities.Concrete
{
    public class Message : Entity, IDateStample
    {
        public int AppUserId { get; set; }
        public AppUser? AppUser { get; set; }

        public int ChatId { get; set; }
        public Chat? Chat { get; set; }

        public string MessageContent { get; set; } = string.Empty;

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
