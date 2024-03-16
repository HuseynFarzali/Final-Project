using SMS.BLL.Data_Transfer_Objects.Main;
using SMS.DAL.Data.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.BLL.Data_Transfer_Objects
{
    public class StudentDto : DataTransferObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<int>? EnrolledCourseIds { get; set; }
        public List<CourseDto>? EnrolledCourses { get; set; }

        public int? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }

        public int? ImageId { get; set; }

        public bool IsDeleted { get; set; }

        public string? CreatedBy { get; set; }
        public string? LastUpdatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class StudentCreateDto : DataTransferObject
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }

    public class StudentUpdateDto : DataTransferObject
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<int>? EnrolledCourseIds { get; set; }
        public int? AppUserId { get; set; }
    }
}
