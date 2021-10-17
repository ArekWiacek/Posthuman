using System.Linq.Expressions;

namespace Posthuman.Core.Repositories
{
    /// <summary>
    /// Generic Repository which is foundation to all other repositories
    /// Provides basic CRUD operations
    /// </summary>
    /// <typeparam name="TEntity">Entity type that will be handled by repository</typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {
        // Obtaining entity
        ValueTask<TEntity> GetByIdAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        
        // Creating entity
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);

        // Updating entity
        Task<TEntity> UpdateAsync(TEntity entity);
        Task UpdateRangeAsync(IEnumerable<TEntity> entities);

        // Removing entity
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
    } 
}
