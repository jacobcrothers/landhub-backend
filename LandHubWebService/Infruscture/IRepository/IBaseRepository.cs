using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Services.Repository
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<string> Create(TEntity obj);
        void Update(TEntity obj);
        void Delete(string id);
        Task<TEntity> Get(string id);
        Task<IEnumerable<TEntity>> Get();
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> criteria);
        Task<TEntity> GetOneAsync(Expression<Func<TEntity, bool>> criteria);
    }
}
