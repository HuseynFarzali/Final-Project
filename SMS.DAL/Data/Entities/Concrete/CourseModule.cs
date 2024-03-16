using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.DAL.Data.Entities.Concrete
{
    public class CourseModule
    {
        public int CourseId { get; set; }
        public Course? Course { get; set; }

        public int ModuleId { get; set; }
        public Module? Module { get; set; }
    }
}
