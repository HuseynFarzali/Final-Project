using SMS.DAL.Data.Entities.Abstract;
using SMS.DAL.Data.Entities.Contracts;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.DAL.Data.Entities.Concrete
{
    public class Course : Entity, ISoftDeletable, IDateStample, IAuthorStample
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Title { get; set; }

        public int Credit { get; set; }

        public List<CourseModule>? CourseModules { get; set; }

        public List<CourseStudent>? CourseStudents { get; set; }
        public List<CourseTeacher>? CourseTeachers { get; set; }

        public int? CategoryId { get; set; }
        public Category? Category { get; set; } 

        public int? BackgroundImageId { get; set; }
        public Image? BackgroundImage { get; set; }

        public bool IsDeleted { get; set; } = false;

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public string? CreatedBy { get; set; }
        public string? LastUpdatedBy { get; set; }
    }
}
