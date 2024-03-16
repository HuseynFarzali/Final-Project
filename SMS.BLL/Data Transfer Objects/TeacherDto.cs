using SMS.BLL.Data_Transfer_Objects.Main;
using SMS.DAL.Data.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.BLL.Data_Transfer_Objects
{
    public class TeacherDto : DataTransferObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string FullName => $"{Name} {Surname}";
        public string? Description { get; set; }

        public List<int>? InstructedCourseIds { get; set; }
        public List<CourseDto>? InstructedCourses { get; set; }

        public int? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }

        public int? ImageId { get; set; }

        public bool IsDeleted { get; set; }

        public string? CreatedBy { get; set; }
        public string? LastUpdatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
    public class TeacherCreateDto : DataTransferObject
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }

    public class TeacherUpdateDto : DataTransferObject
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<int>? InstructedCourseIds { get; set; }
        public int? AppUserId { get; set; }
    }
}
