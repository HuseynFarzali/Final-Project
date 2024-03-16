using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using SMS.DAL.Data.Entities.Abstract;
using SMS.Tools.Tools;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SMS.DAL.Data.Entities.Contracts;
using SMS.DAL.Data.Database_Context;
using SMS.DAL.Repositories.Main.Contracts;
using SMS.WebTools.Attributes;

namespace SMS.DAL.Repositories.Main
{
    [Injectible(
        Lifetime = Microsoft.Extensions.DependencyInjection.ServiceLifetime.Scoped,
        Implements = typeof(IRepository<,>))]
    public class CoreRepository<TEntity, TContext> : IRepository<TEntity, TContext>
        where TEntity : Entity
        where TContext : DbContext
    {
        //private readonly IAppAuthenticationService _authService;
        protected TContext _context;
        protected DbSet<TEntity> _entities;

        public CoreRepository(TContext context)
        {
            _context = context;
            _entities = _context.Set<TEntity>();
        }

        #region Add methods
        public virtual async Task Add(TEntity entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            if (typeof(TEntity).GetInterface(nameof(IDateStample)) is not null)
            {
                var dateStampled = (IDateStample)entity;
                dateStampled.CreatedAt ??= DateTime.UtcNow;
                dateStampled.UpdatedAt ??= DateTime.UtcNow;
            }

            if (typeof(TEntity).GetInterface(nameof(IAuthorStample)) is not null)
            {
                var authorStampled = (IAuthorStample)entity;
                authorStampled.CreatedBy ??= "super_admin_will_be_changed_to_user_entity";
                authorStampled.LastUpdatedBy ??= "super_admin_will_be_changed_to_user_entity";
            }

            await _entities.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public virtual async Task AddRange(IEnumerable<TEntity> entities)
        {
            if (entities is null)
                throw new ArgumentNullException(nameof(entities));

            foreach (var entity in entities)
                await Add(entity);
        }
        public virtual async Task<Result> TryAdd(TEntity entity)
        {
            try
            {
                await Add(entity);
            }
            catch (Exception ex)
            {
                return Result.In(ex);
            }

            return Result.Success;
        }
        public async Task<Result> TryAddRange(IEnumerable<TEntity> entities)
        {
            try
            {
                await AddRange(entities);
            }
            catch (Exception ex)
            {
                return Result.In(ex);
            }

            return Result.Success;
        }
        #endregion
        #region Get methods
        public virtual async Task<TEntity> Get(
            int id,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includePathQuery = null,
            bool enableTracking = false)
        {
            var query = _entities.AsQueryable();
            IIncludableQueryable<TEntity, object>? includableQuery = null;

            if (includePathQuery is not null)
                includableQuery = includePathQuery(query);

            if (!enableTracking)
                query = query.AsNoTracking();

            var entity = await query.FirstOrDefaultAsync(entity => entity.Id == id);

            if (entity is null)
                throw new NullReferenceException(nameof(entity));

            return entity;
        }
        public async Task<IEnumerable<TEntity>> GetRange(
            Expression<Func<TEntity, bool>>? predicate = null,
            Expression<Func<TEntity, object>>? orderBy = null,
            Expression<Func<TEntity, object>>? orderByDescending = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includePathQuery = null,
            bool enableTracking = false)
        {
            var query = _entities.AsQueryable();

            IIncludableQueryable<TEntity, object>? includableQuery = null;

            if (predicate is not null)
                query = query.Where(predicate);

            if (orderBy is not null)
                query = query.OrderBy(orderBy);

            else if (orderByDescending is not null)
                query = query.OrderByDescending(orderByDescending);

            if (!enableTracking)
                query = query.AsNoTracking();

            if (includePathQuery is not null)
                includableQuery = includePathQuery(query);

            var enumerableEntities = includableQuery is null
                ? await query.ToListAsync()
                : await includableQuery.ToListAsync();

            return enumerableEntities;
        }
        public virtual async Task<(TEntity?, Result)> TryGet(
            int id,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includePathQuery = null,
            bool enableTracking = false)
        {
            TEntity? foundEntity = null;
            try
            {
                foundEntity = await Get(id, includePathQuery, enableTracking);
            }
            catch (Exception ex)
            {
                return (foundEntity, Result.In(ex));
            }

            return (foundEntity, Result.Success);
        }
        public virtual async Task<(IEnumerable<TEntity>?, Result)> TryGetRange(
            Expression<Func<TEntity, bool>>? predicate = null,
            Expression<Func<TEntity, object>>? orderBy = null,
            Expression<Func<TEntity, object>>? orderByDescending = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includePathQuery = null,
            bool enableTracking = false)
        {
            IEnumerable<TEntity>? foundEntities = null;
            try
            {
                foundEntities = await GetRange(predicate, orderBy, orderByDescending, includePathQuery, enableTracking);
            }
            catch (Exception ex)
            {
                return (foundEntities, Result.In(ex));
            }

            return (foundEntities, Result.Success);
        }
        #endregion
        #region Update methods
        public virtual async Task Update(int id, TEntity entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            var currentEntity = await Get(id);

            if (currentEntity is null)
                throw new NullReferenceException(nameof(currentEntity));

            entity.Id = currentEntity.Id;

            if (typeof(TEntity).GetInterface(nameof(IDateStample)) is not null)
            {
                var dateStampled = (IDateStample)entity;
                dateStampled.CreatedAt = ((IDateStample)currentEntity).CreatedAt;
                dateStampled.UpdatedAt ??= DateTime.UtcNow;
            }

            if (typeof(TEntity).GetInterface(nameof(IAuthorStample)) is not null)
            {
                var authorStampled = (IAuthorStample)entity;
                authorStampled.CreatedBy = ((IAuthorStample)currentEntity).CreatedBy;
                authorStampled.LastUpdatedBy ??= "super_admin_will_be_changed_to_user_entity";
            }

            _entities.Update(entity);
            await _context.SaveChangesAsync();
        }
        public virtual async Task<Result> TryUpdate(int id, TEntity entity)
        {
            try
            {
                await Update(id, entity);
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
            var foundEntity = await _entities.FindAsync(id)
                ?? throw new ArgumentException();

            if (typeof(TEntity).GetInterface(nameof(ISoftDeletable)) is not null)
            {
                var softDeletableForm = (ISoftDeletable)foundEntity;
                softDeletableForm.IsDeleted = true;
            }
            else
                _entities.Remove(foundEntity);

            await _context.SaveChangesAsync();
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
        public virtual async Task DeleteRange(Func<TEntity, bool> predicate)
        {
            var foundEntities = _entities.Where(predicate);
            if (foundEntities.Any())
            {
                foreach (var entity in foundEntities)
                {
                    await Delete(entity.Id);
                }
            }
        }
        public virtual async Task<Result> TryDeleteRange(Func<TEntity, bool> predicate)
        {
            try
            {
                await DeleteRange(predicate);
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
