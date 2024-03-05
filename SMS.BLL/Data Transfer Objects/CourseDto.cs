using SMS.BLL.Data_Transfer_Objects.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.BLL.Data_Transfer_Objects
{
    public class CourseDto : DataTransferObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Credit { get; set; }
        public List<int>? EnrolledStudentIds { get; set; }
        public List<StudentDto>? EnrolledStudents { get; set; } 

        public bool IsDeleted { get; set; }

        public string? CreatedBy { get; set; }
        public string? LastUpdatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class CourseCreateDto : DataTransferObject
    {
        public string Name { get; set; }
        public int Credit { get; set; }
    }

    public class CourseUpdateDto : DataTransferObject
    {
        public string Name { get; set; }
        public int Credit { get; set; }

        public List<int>? EnrolledStudentIds { get; set; }
    }
}
