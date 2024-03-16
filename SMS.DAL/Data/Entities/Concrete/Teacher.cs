using SMS.DAL.Data.Entities.Abstract;
using SMS.DAL.Data.Entities.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.DAL.Data.Entities.Concrete
{
    public class Teacher : Entity, IAuthorStample, IDateStample, ISoftDeletable, IAccountBindable
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string FullName => $"{Name} {Surname}";

        public string? Description { get; set; }

        public List<CourseTeacher>? CourseTeachers { get; set; }

        public int? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }

        public int? ImageId { get; set; }
        public Image? Image { get; set; }

        public bool Bound { get; set; } = false;

        public string? CreatedBy { get; set; }
        public string? LastUpdatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
