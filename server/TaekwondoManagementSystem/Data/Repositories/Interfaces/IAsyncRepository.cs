using System.Linq.Expressions;

namespace Data.Repositories.Interfaces
{
    public interface IAsyncRepository<TEntity> where TEntity : class
    {
        Task<TEntity> AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        Task UpdateAsync(TEntity entity);
        Task UpdateRangeAsync(IEnumerable<TEntity> entities);
        Task RemoveAsync(TEntity entity);
        Task RemoveRangeAsync(IEnumerable<TEntity> entities);
        Task<int> CountAsync();
        Task<TEntity> GetByIdAsync(int id);
        Task<TEntity> GetSingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        Task<ICollection<TEntity>> ListAllAsync();
        Task<ICollection<TEntity>> ListAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
