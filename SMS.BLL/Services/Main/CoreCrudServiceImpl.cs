using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using SMS.BLL.Data_Transfer_Objects.Main;
using SMS.BLL.Services.Main.Contracts;
using SMS.DAL.Data.Database_Context;
using SMS.DAL.Data.Entities.Abstract;
using SMS.DAL.Repositories.Main.Contracts;
using SMS.Tools.Enums;
using SMS.Tools.Tools;
using SMS.WebTools.Attributes;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SMS.BLL.Services.Main
{
    [Injectible(
        Lifetime = Microsoft.Extensions.DependencyInjection.ServiceLifetime.Scoped,
        Implements = typeof(ICoreCrudService<,,,,>))]
    public class CoreCrudServiceImpl<TEntity, TDto, TCreateDto, TUpdateDto, TContext>
        : ICoreCrudService<TEntity, TDto, TCreateDto, TUpdateDto, TContext>
        where TEntity : Entity
        where TDto : DataTransferObject
        where TCreateDto : DataTransferObject
        where TUpdateDto : DataTransferObject
        where TContext : DbContext
    {
        protected readonly IMapper _mapper;
        protected readonly IRepository<TEntity, TContext> _repository;

        public CoreCrudServiceImpl(IMapper mapper, IRepository<TEntity, TContext> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        #region Before methods
        public virtual Task<Result> TryBeforeAdd(TEntity entity) { return Task.FromResult(Result.Success); }

        public virtual Task<Result> TryBeforeUpdate(int id, TEntity entity) { return Task.FromResult(Result.Success); }

        public virtual Task<Result> TryBeforeDelete(int id) { return Task.FromResult(Result.Success); }
        #endregion

        #region Add methods
        public virtual async Task Add(TCreateDto dto)
        {
            if (dto is null)
                throw new ArgumentNullException(nameof(dto));

            var entity = _mapper.Map<TEntity>(dto);

            var result = await TryBeforeAdd(entity);

            if (result.State != ResultState.Success)
                throw new OperationCanceledException("BeforeAdd fail-ended");

            await _repository.Add(entity);
        }
        public virtual async Task<Result> TryAdd(TCreateDto dto)
        {
            try
            {
                await Add(dto);
            }
            catch(Exception ex)
            {
                return Result.In(ex);
            }

            return Result.Success;
        }

        public virtual async Task AddRange(IEnumerable<TCreateDto> dtos)
        {
            if (dtos is null)
                throw new ArgumentNullException(nameof(dtos));

            foreach (var dto in dtos)
            {
                await Add(dto);
            }
        }
        public virtual async Task<Result> TryAddRange(IEnumerable<TCreateDto> dtos)
        {
            try
            {
                await AddRange(dtos);
            }
            catch (Exception ex)
            {
                return Result.In(ex);
            }

            return Result.Success;
        }
        #endregion
        #region Get methods
        public virtual async Task<TDto> Get(
            int id,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includePathQuery = null,
            bool enableTracking = false)
        {
            var entity = await _repository.Get(id, includePathQuery, enableTracking);

            if (entity is null)
                throw new ArgumentException(nameof(entity));

            var entityDto = _mapper.Map<TDto>(entity);
            return entityDto;
        }
        public virtual async Task<(TDto, Result)> TryGet(
            int id,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includePathQuery = null,
            bool enableTracking = false)
        {
            TDto entityDto = null;
            try
            {
                entityDto = await Get(id, includePathQuery, enableTracking);
            }
            catch (Exception ex)
            {
                return (entityDto, Result.In(ex));
            }

            return (entityDto, Result.Success);
        }

        public virtual async Task<IEnumerable<TDto>> GetRange(
            Expression<Func<TEntity, bool>>? predicate = null,
            Expression<Func<TEntity, object>>? orderBy = null,
            Expression<Func<TEntity, object>>? orderByDescending = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includePathQuery = null,
            bool enableTracking = false)
        {
            var entities = await _repository.GetRange(predicate, orderBy, orderByDescending, includePathQuery, enableTracking);

            if (entities is null)
                throw new ArgumentException(nameof(entities));

            var entityDtos = _mapper.Map<IEnumerable<TDto>>(entities);
            return entityDtos;
        }

        public virtual async Task<(IEnumerable<TDto>, Result)> TryGetRange(
            Expression<Func<TEntity, bool>>? predicate = null,
            Expression<Func<TEntity, object>>? orderBy = null,
            Expression<Func<TEntity, object>>? orderByDescending = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includePathQuery = null,
            bool enableTracking = false)
        {
            IEnumerable<TDto> entityDtos = null;
            try
            {
                entityDtos = await GetRange(predicate, orderBy, orderByDescending, includePathQuery, enableTracking);
            }
            catch (Exception ex)
            {
                return (entityDtos, Result.In(ex));
            }

            return (entityDtos, Result.Success);
        }
        #endregion
        #region Update methods
        public virtual async Task Update(int id, TUpdateDto dto)
        {
            _ = await _repository.Get(id)
                ?? throw new ArgumentException(nameof(id));

            var updatedEntity = _mapper.Map<TEntity>(dto);

            var result = await TryBeforeUpdate(id, updatedEntity);

            if (result.State != Tools.Enums.ResultState.Success)
                throw new OperationCanceledException("BeforeUpdate fail-ended");

            await _repository.Update(id, updatedEntity);
        }
        public virtual async Task<Result> TryUpdate(int id, TUpdateDto dto)
        {
            try
            {
                await Update(id, dto);
            }
            catch (Exception ex)
            {
                return Result.In(ex);
            }

            return Result.Success;
        }
        #endregion
        #region Delete methods
        public virtual async Task Delete(int id)
        {
            var result = await TryBeforeDelete(id);
            if (result != Result.Success)
                throw new OperationCanceledException("BeforeDelete fail-ended");

            await _repository.Delete(id);
        }
        public virtual async Task<Result> TryDelete(int id)
        {
            try
            {
                await Delete(id);
            }
            catch (Exception ex)
            {
                return Result.In(ex);
            }

            return Result.Success;
        }
        #endregion
    }
}
