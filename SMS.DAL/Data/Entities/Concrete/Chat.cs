using SMS.DAL.Data.Entities.Abstract;
using SMS.DAL.Data.Entities.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.DAL.Data.Entities.Concrete
{
    public class Chat : Entity, ISoftDeletable, IDateStample
    {
        public string? Name { get; set; }

        public List<int>? MessageIds { get; set; }
        public List<Message>? Messages { get; set; }

        public List<ChatUser>? ChatUsers { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
