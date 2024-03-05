using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using SMS.DAL.Data.Entities.Abstract;
using SMS.Tools.Tools;
using System.Linq.Expressions;

namespace SMS.DAL.Repositories.Main.Contracts
{
    public interface IRepository<TEntity, TContext>
        where TEntity : Entity
        where TContext : DbContext
    {
        /// <summary>
        /// Adds the <paramref name="entity"/> to the data context.
        /// </summary>
        /// <param name="entity">Entity reference to add.</param>
        /// <returns></returns>
        Task Add(TEntity entity);

        /// <summary>
        /// Adds the <paramref name="entity"/> to the data context.
        /// </summary>
        /// <param name="entity">Entity reference to add.</param>
        /// <returns><see cref="Result"/> instance representing the final state of the operation.</returns>
        Task<Result> TryAdd(TEntity entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task AddRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Adds an <seealso cref="IEnumerable{TEntity}"/> collection of entities to the data context.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns><see cref="Result"/> instance representing the final state of the operation.</returns>
        Task<Result> TryAddRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Gets an instance with <paramref name="id"/> value from the data context.
        /// </summary>
        /// <param name="id">ID value to find the matching instance.</param>
        /// <returns><typeparamref name="TEntity"/> type instance from the data context.</returns>
        Task<TEntity> Get(
            int id,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includePathQuery = null,
            bool enableTracking = false);

        Task<IEnumerable<TEntity>> GetRange(
            Expression<Func<TEntity, bool>>? predicate = null,
            Expression<Func<TEntity, object>>? orderBy = null,
            Expression<Func<TEntity, object>>? orderByDescending = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includePathQuery = null,
            bool enableTracking = false);

        /// <summary>
        /// Gets an wrapper including <typeparamref name="TEntity"/> type entity and <typeparamref name="TResult"/> type final state of the operation.
        /// </summary>
        /// <param name="id">ID value to find the matching instance.</param>
        /// <returns>Tuple type wrapper including the found <typeparamref name="TEntity"/> entity and <see cref="Result"/> final state of the operation.</returns>
        Task<(TEntity?, Result)> TryGet(
            int id,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includePathQuery = null,
            bool enableTrackinge = false);

        Task<(IEnumerable<TEntity>?, Result)> TryGetRange(
            Expression<Func<TEntity, bool>>? predicate = null,
            Expression<Func<TEntity, object>>? orderBy = null,
            Expression<Func<TEntity, object>>? orderByDescending = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? includePathQuery = null,
            bool enableTracking = false);

        /// <summary>
        /// Updates the <typeparamref name="TEntity"/> type instance in the data context.
        /// </summary>
        /// <param name="entity"><typeparamref name="TEntity"/> type instance representing the new form of entity to overwrite.</param>
        /// <returns></returns>
        Task Update(int id, TEntity entity);

        /// <summary>
        /// Updates the <typeparamref name="TEntity"/> type instance in the data context.
        /// </summary>
        /// <param name="entity"><paramref name="entity"/> type instance representing the new form of entity to overwrite.</param>
        /// <returns><see cref="Result"/> instance representing the final state of the operation.</returns>
        Task<Result> TryUpdate(int id, TEntity entity);

        /// <summary>
        /// Deletes the <typeparamref name="TEntity"/> type entity from the data context.
        /// </summary>
        /// <param name="id">ID value to find the matching instance.</param>
        /// <returns></returns>
        Task Delete(int id);

        /// <summary>
        /// Deletes the <typeparamref name="TEntity"/> type entity from the data context.
        /// </summary>
        /// <param name="id"></param>
        /// <returns><see cref="Result"/> instance representing the final state of the operation.</returns>
        Task<Result> TryDelete(int id);

        /// <summary>
        /// Deletes the <see cref="IEnumerable{TEntity}"/> list from the data context.
        /// </summary>
        /// <param name="predicate">Match for selecting entities for deletion.</param>
        /// <returns></returns>
        Task DeleteRange(Func<TEntity, bool> predicate);

        /// <summary>
        /// Deletes the <see cref="IEnumerable{TEntity}"/> list from the data context.
        /// </summary>
        /// <param name="predicate">Match for selecting entities for deletion.</param>
        /// <returns><see cref="Result"/> instance representing the final state of the operation.</returns>
        Task<Result> TryDeleteRange(Func<TEntity, bool> predicate);
    }
}
