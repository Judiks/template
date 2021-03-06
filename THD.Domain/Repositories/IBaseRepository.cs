﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace THD.Domain.Repositories
{
    public interface IBaseRepository<TEntity>
    {
        Task<TEntity> GetById(string id);
        Task<List<TEntity>> GetAll();
        Task<string> Add(TEntity entity);
        Task<int> AddRange(List<TEntity> entities);
        Task Update(TEntity entity);
        Task UpdateRange(List<TEntity> entity);
        Task<string> Delete(TEntity entity);
        Task DeleteRange(ICollection<TEntity> entities);
    }
}
