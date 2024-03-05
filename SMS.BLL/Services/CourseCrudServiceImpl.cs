using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using SMS.BLL.Data_Transfer_Objects;
using SMS.BLL.Services.Contracts;
using SMS.BLL.Services.Main;
using SMS.DAL.Data.Database_Context;
using SMS.DAL.Data.Entities.Concrete;
using SMS.DAL.Repositories.Main.Contracts;
using SMS.Tools.Tools;
using SMS.WebTools.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.BLL.Services
{
    [Injectible(
        Lifetime = ServiceLifetime.Scoped,
        Implements = typeof(ICourseCrudService))]
    public class CourseCrudServiceImpl : CoreCrudServiceImpl<Course, CourseDto, CourseCreateDto, CourseUpdateDto, CoreDbContext>, ICourseCrudService
    {
        public CourseCrudServiceImpl(IMapper mapper, IRepository<Course, CoreDbContext> repository) : base(mapper, repository)
        {
        }
    }
}
