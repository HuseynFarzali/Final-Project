using SMS.BLL.Data_Transfer_Objects.Main;
using SMS.DAL.Data.Entities.Concrete;
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
        public string? Description { get; set; }
        public string? Title { get; set; }
        public int Credit { get; set; }

        public int? LessonCount { get; set; }
        public TimeSpan? Duration { get; set; }

        public List<int>? EnrolledStudentIds { get; set; }
        public List<StudentDto>? EnrolledStudents { get; set; }

        public List<int>? InstructedTeacherIds { get; set; }
        public List<TeacherDto>? InstructedTeachers { get; set; }

        public List<int>? ModuleIds { get; set; }
        public List<ModuleDto>? Modules { get; set; }

        public int? CategoryId { get; set; }
        public CategoryDto? Category { get; set; }

        public int? BackgroundImageId { get; set; }

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
        public string? Description { get; set; }
        public string? Title { get; set; }
    }

    public class CourseUpdateDto : DataTransferObject
    {
        public string Name { get; set; }
        public int Credit { get; set; }
        public string? Description { get; set; }
        public string? Title { get; set; }

        public List<int>? EnrolledStudentIds { get; set; }
        public List<int>? InstructedTeacherIds { get; set; }
        public List<int>? ModuleIds { get; set; }

        public int? CategoryId { get; set; }
    }
}
