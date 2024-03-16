using SMS.BLL.Data_Transfer_Objects.Main;
using SMS.DAL.Data.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.BLL.Data_Transfer_Objects
{
    public class MessageDto : DataTransferObject
    {
        public int Id { get; set; }
        public int AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        public string MessageContent { get; set; }

        public int ChatId { get; set; }
        public ChatDto? Chat { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class MessageCreateDto : DataTransferObject
    {
        public int AppUserId { get; set; }
        public int ChatId { get; set; }
        public string? MessageContent { get; set; }
    }

    public class MessageUpdateDto : DataTransferObject
    {
        public int AppUserId { get; set; }
        public int ChatId { get; set; }
        public string? MessageContent { get; set; }
    }
}
