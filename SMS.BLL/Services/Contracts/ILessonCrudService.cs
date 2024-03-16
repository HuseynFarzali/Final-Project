using SMS.BLL.Data_Transfer_Objects;
using SMS.BLL.Services.Main.Contracts;
using SMS.DAL.Data.Database_Context;
using SMS.DAL.Data.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.BLL.Services.Contracts
{
    public interface ILessonCrudService : ICoreCrudService<Lesson, LessonDto, LessonCreateDto, LessonUpdateDto, CoreDbContext>
    {
    }
}
