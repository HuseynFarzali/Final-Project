using SMS.BLL.Data_Transfer_Objects.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.BLL.Data_Transfer_Objects
{
    public class LessonDto : DataTransferObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public TimeSpan Duration { get; set; }

        public List<ModuleDto>? UsedInModules { get; set; }

        public bool IsDeleted { get; set; }
        public string? CreatedBy { get; set; }
        public string? LastUpdatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class LessonCreateDto : DataTransferObject
    {
        public string Name { get; set; }
        public string? Description { get; set; }

        //public TimeSpan DurationTime { get; set; }
        public int DurationHours { get; set; }
        public int DurationMinutes { get; set; }
    }

    public class LessonUpdateDto : DataTransferObject
    {
        public string Name { get; set; }
        public string? Description { get; set; }

        public List<int>? UsedInModuleIds { get; set; }

        //public TimeSpan DurationTime { get; set; }
        public int DurationHours { get; set; }
        public int DurationMinutes { get; set; }
    }
}
