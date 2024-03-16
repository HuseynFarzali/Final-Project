using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.DAL.Data.Entities.Concrete
{
    public class LessonModule
    {
        public int ModuleId { get; set; }
        public Module? Module { get; set; }

        public int LessonId { get; set; }
        public Lesson? Lesson { get; set; }
    }
}
