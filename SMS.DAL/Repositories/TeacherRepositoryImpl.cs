using SMS.DAL.Data.Database_Context;
using SMS.DAL.Data.Entities.Concrete;
using SMS.DAL.Repositories.Contracts;
using SMS.DAL.Repositories.Main;
using SMS.WebTools.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.DAL.Repositories
{
    [Injectible(
    Lifetime = Microsoft.Extensions.DependencyInjection.ServiceLifetime.Scoped,
    Implements = typeof(ITeacherRepository))]
    public class TeacherRepositoryImpl : CoreRepository<Teacher, CoreDbContext>, ITeacherRepository
    {
        public TeacherRepositoryImpl(CoreDbContext context) : base(context)
        {
        }
    }
}
