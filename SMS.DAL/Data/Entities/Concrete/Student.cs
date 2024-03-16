using SMS.DAL.Data.Entities.Abstract;
using SMS.DAL.Data.Entities.Contracts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.DAL.Data.Entities.Concrete
{
    public class Student : Entity, ISoftDeletable, IAuthorStample, IDateStample, IAccountBindable
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        public List<CourseStudent>? CourseStudents { get; set; }

        public int? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        public bool Bound { get; set; } = false;

        public int? ImageId { get; set; }
        public Image? Image { get; set; }

        public bool IsDeleted { get; set; } = false;

        public string? CreatedBy { get; set; }
        public string? LastUpdatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }   
}
