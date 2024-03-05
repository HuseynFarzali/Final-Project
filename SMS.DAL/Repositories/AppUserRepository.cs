using SMS.DAL.Data.Database_Context;
using SMS.DAL.Data.Entities.Abstract;
using SMS.DAL.Data.Entities.Concrete;
using SMS.DAL.Repositories.Contracts;
using SMS.DAL.Repositories.Main;
using SMS.Tools.Enums;
using SMS.WebTools.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SMS.DAL.Repositories
{
    [Injectible(
        Lifetime = Microsoft.Extensions.DependencyInjection.ServiceLifetime.Scoped,
        Implements = typeof(IAppUserRepository))]
    public class AppUserRepository : CoreRepository<AppUser, CoreDbContext>, IAppUserRepository
    {
        public AppUserRepository(CoreDbContext context) : base(context)
        {
        }
    }
}
