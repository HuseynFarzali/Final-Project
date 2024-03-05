using SMS.DAL.Data.Entities.Abstract;
using SMS.DAL.Data.Entities.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.DAL.Data.Entities.Concrete
{
    public class Category : Entity, IDateStample, IAuthorStample, ISoftDeletable
    {
        public string Name { get; set; }
        public string? Description { get; set; }

        public List<Course>? Courses { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public string? LastUpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
