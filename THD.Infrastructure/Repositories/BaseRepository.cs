using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using THD.Domain.Entities;
using THD.Domain.Repositories;

namespace THD.Infrastructure.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        public ApplicationDbContext _applicationDbContext;

        protected DbSet<TEntity> _dbSet;

        public BaseRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _dbSet = _applicationDbContext.Set<TEntity>();
        }

        public async Task<TEntity> GetById(string id)
        {
            return await _dbSet.Where(entity => entity.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<string> Add(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _applicationDbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<int> AddRange(List<TEntity> entities)
        {
            _dbSet.AddRange(entities);
            await _applicationDbContext.SaveChangesAsync();
            return entities.Count;
        }

        public async Task Update(TEntity entity)
        {
            _dbSet.Update(entity);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task UpdateRange(List<TEntity> entity)
        {
            _dbSet.UpdateRange(entity);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task<string> Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
            await _applicationDbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task DeleteRange(ICollection<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}
