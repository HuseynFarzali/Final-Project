using SMS.BLL.Data_Transfer_Objects.Main;
using SMS.DAL.Data.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.BLL.Data_Transfer_Objects
{
    public class CategoryDto : DataTransferObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public List<CourseDto>? Courses { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? LastUpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class CategoryCreateDto : DataTransferObject
    {
        public string Name { get; set; }
        public string? Description { get; set; }
    }

    public class CategoryUpdateDto : DataTransferObject
    {
        public string Name { get; set; }
        public string? Description { get; set; }

        public List<int>? CoursesIds { get; set; }
    }
}
