using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Services.Repository
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<string> Create(TEntity obj);
        Task<bool> UpdateAsync(TEntity obj);
        Task Delete(string id);
        Task DeleteAllAsync(Expression<Func<TEntity, bool>> criteria);
        Task<TEntity> GetByIdAsync(string id);
        Task<IEnumerable<TEntity>> GetAsync();
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> criteria);
        Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> criteria);
    }
}
