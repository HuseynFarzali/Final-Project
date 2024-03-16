using SMS.DAL.Data.Entities.Abstract;
using SMS.DAL.Data.Entities.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.DAL.Data.Entities.Concrete
{
    public class Lesson : Entity, ISoftDeletable, IAuthorStample, IDateStample
    {
        public string Name { get; set; }
        public string? Description { get; set; }

        public TimeSpan Duration { get; set; }

        public List<LessonModule>? LessonModules { get; set; }

        public bool IsDeleted { get; set; } = false;
        public string? CreatedBy { get; set; }
        public string? LastUpdatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
