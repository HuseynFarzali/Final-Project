using SMS.BLL.Data_Transfer_Objects.Main;
using SMS.DAL.Data.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.BLL.Data_Transfer_Objects
{
    public class ChatDto : DataTransferObject
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public List<MessageDto> Messages { get; set; }
        public List<AppUser>? AppUsers { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
    
    public class ChatCreateDto : DataTransferObject
    {
        public string? Name { get; set; }
        public List<int> AppUserIds { get; set; }
    }

    public class ChatUpdateDto : DataTransferObject
    {
        public string? Name { get; set; }
        public List<int> AppUserIds { get; set; }
    }
}
