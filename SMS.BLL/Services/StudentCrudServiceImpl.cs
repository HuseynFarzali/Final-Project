using AutoMapper;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using SMS.Authentication.Services.Contracts;
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
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SMS.BLL.Services
{
    [Injectible(
        Lifetime = Microsoft.Extensions.DependencyInjection.ServiceLifetime.Scoped,
        Implements = typeof(IStudentCrudService))]
    public class StudentCrudServiceImpl : CoreCrudServiceImpl<Student, StudentDto, StudentCreateDto, StudentUpdateDto, CoreDbContext>, IStudentCrudService
    {
        private readonly IAppUserRepository _userRepository;
        private readonly IAppAuthenticationService _authService;

        public StudentCrudServiceImpl(IMapper mapper, IRepository<Student, CoreDbContext> repository, IAppUserRepository userRepository, IAppAuthenticationService authService) : base(mapper, repository)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        public override async Task<Result> TryBeforeUpdate(int id, Student updatedEntity)
        {
            if (updatedEntity.GetType().GetInterface(nameof(IAccountBindable)) is not null)
            {
                var currentEntity = await _repository.Get(id);
                if (currentEntity.AppUserId is not null)
                    return Result.Success;

                if (updatedEntity.AppUserId.HasValue)
                {
                    var entityExistWithCurrentAppUserId = (await _repository.GetRange(predicate: student => student.AppUserId == updatedEntity.AppUserId)).Any();

                    if (entityExistWithCurrentAppUserId) return Result.In(Tools.Enums.ResultState.Fail, "Cannot bind more than one student with a single AppUser account.");

                    var matchingAppUser = await _userRepository.Get(updatedEntity.AppUserId.Value, enableTracking: true);
                    matchingAppUser.UserType = Tools.Enums.UserType.Student;
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
