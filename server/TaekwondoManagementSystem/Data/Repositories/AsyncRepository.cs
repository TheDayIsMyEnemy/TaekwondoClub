using Data;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public abstract class AsyncRepository<TEntity> : IAsyncRepository<TEntity> where TEntity : class
    {
        protected readonly AppDbContext _dbContext;
        protected readonly DbSet<TEntity> _entities;

        public AsyncRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _entities = _dbContext.Set<TEntity>();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            _entities.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            _entities.AddRange(entities);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<int> CountAsync()
        {
            return await _entities.CountAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _entities.FindAsync(id);
        }

        public async Task<TEntity> GetSingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _entities.SingleOrDefaultAsync(predicate);
        }

        public async Task<ICollection<TEntity>> ListAllAsync()
        {
            return await _entities.ToListAsync();
        }

        public async Task<ICollection<TEntity>> ListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _entities.Where(predicate).ToListAsync();
        }

        public async Task RemoveAsync(TEntity entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveRangeAsync(IEnumerable<TEntity> entities)
        {
            _dbContext.RemoveRange(entities);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _entities.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            _entities.UpdateRange(entities);
            await _dbContext.SaveChangesAsync();
        }
    }
}