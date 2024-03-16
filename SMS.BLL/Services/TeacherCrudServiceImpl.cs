using AutoMapper;
using SMS.BLL.Data_Transfer_Objects;
using SMS.BLL.Services.Contracts;
using SMS.BLL.Services.Main;
using SMS.DAL.Data.Database_Context;
using SMS.DAL.Data.Entities.Concrete;
using SMS.DAL.Data.Entities.Contracts;
using SMS.DAL.Repositories.Contracts;
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
        Lifetime = Microsoft.Extensions.DependencyInjection.ServiceLifetime.Scoped,
        Implements = typeof(ITeacherCrudService))]
    public class TeacherCrudServiceImpl : CoreCrudServiceImpl<Teacher, TeacherDto, TeacherCreateDto, TeacherUpdateDto, CoreDbContext>, ITeacherCrudService
    {
        private readonly IAppUserRepository _userRepository;

        public TeacherCrudServiceImpl(IMapper mapper, IRepository<Teacher, CoreDbContext> repository, IAppUserRepository userRepository) : base(mapper, repository)
        {
            _userRepository = userRepository;
        }

        public override async Task<Result> TryBeforeUpdate(int id, Teacher updatedEntity)
        {
            if (updatedEntity.GetType().GetInterface(nameof(IAccountBindable)) is not null)
            {
                var currentEntity = await _repository.Get(id);
                if (currentEntity.AppUserId is not null)
                {
                    return Result.Success;
                }

                if (updatedEntity.AppUserId.HasValue)
                {
                    var entityExistWithCurrentAppUserId = (await _repository.GetRange(predicate: teacher => teacher.AppUserId == updatedEntity.AppUserId)).Any();

                    if (entityExistWithCurrentAppUserId) return Result.In(Tools.Enums.ResultState.Fail, "Cannot bind more than one teacher with a single AppUser account.");

                    var matchingAppUser = await _userRepository.Get(updatedEntity.AppUserId.Value, enableTracking: true);
                    matchingAppUser.UserType = Tools.Enums.UserType.Teacher;
                    updatedEntity.Bound = true;
                }
            }

            return Result.Success;
        }

        public override async Task<Result> TryBeforeDelete(int id)
        {
            var currentEntity = await _repository.Get(id, enableTracking: true);
            if (currentEntity.AppUserId is not null)
            {
                var currentAppUser = await _userRepository.Get(currentEntity.AppUserId.Value);
                currentAppUser.UserType = Tools.Enums.UserType.None;
            }

            currentEntity.Bound = false;
            currentEntity.AppUserId = null;

            return Result.Success;
        }
    }
}
