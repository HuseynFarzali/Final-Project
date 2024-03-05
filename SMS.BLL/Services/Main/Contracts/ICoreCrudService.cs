using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using SMS.BLL.Data_Transfer_Objects.Main;
using SMS.DAL.Data.Entities.Abstract;
using SMS.Tools.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SMS.BLL.Services.Main.Contracts
{
    public interface ICoreCrudService<TEntity, TDto, TCreateDto, TUpdateDto, TContext>
        where TEntity : Entity
        where TDto : DataTransferObject
        where TCreateDto : DataTransferObject
        where TUpdateDto : DataTransferObject
        where TContext : DbContext
    {
        Task<TDto> Get(
            int id,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includePathQuery = null,
            bool enableTracking = false);
        Task<(TDto, Result)> TryGet(
            int id,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includePathQuery = null,
            bool enableTracking = false);

        Task<IEnumerable<TDto>> GetRange(
            Expression<Func<TEntity, bool>>? predicate = null,
            Expression<Func<TEntity, object>>? orderBy = null,
            Expression<Func<TEntity, object>>? orderByDescending = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includePathQuery = null,
            bool enableTracking = false);
        Task<(IEnumerable<TDto>, Result)> TryGetRange(
            Expression<Func<TEntity, bool>>? predicate = null,
            Expression<Func<TEntity, object>>? orderBy = null,
            Expression<Func<TEntity, object>>? orderByDescending = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includePathQuery = null,
            bool enableTracking = false);

        Task Add(TCreateDto dto);
        Task<Result> TryAdd(TCreateDto dto);

        Task AddRange(IEnumerable<TCreateDto> dtos);
        Task<Result> TryAddRange(IEnumerable<TCreateDto> dtos);

        Task Update(int id, TUpdateDto dto);
        Task<Result> TryUpdate(int id, TUpdateDto dto);

        Task Delete(int id);
        Task<Result> TryDelete(int id);
    }
}
