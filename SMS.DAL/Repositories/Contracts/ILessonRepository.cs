﻿using SMS.DAL.Data.Database_Context;
using SMS.DAL.Data.Entities.Concrete;
using SMS.DAL.Repositories.Main.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.DAL.Repositories.Contracts
{
    public interface ILessonRepository : IRepository<Lesson, CoreDbContext>
    {
    }
}
