using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SimpleKanban.DB.Abstract
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class, IEntity
    {
        TEntity FindById(int id);
        IQueryable<TEntity> Get();
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate);
        int Count();
        int Count(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] properties);
        IQueryable<TEntity> GetWithInclude(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] properties);

        // ASYNC
        Task<TEntity> FindByIdAsync(int id);
        Task<IQueryable<TEntity>> GetAsync();
        Task<IQueryable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate);
        Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IQueryable<TEntity>> GetWithIncludeAsync(params Expression<Func<TEntity, object>>[] properties);
        Task<IQueryable<TEntity>> GetWithIncludeAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] properties);

        void Create(TEntity item);
        void Update(TEntity item);
        void Remove(TEntity item);

        Task CreateAsync(TEntity item);
        Task UpdateAsync(TEntity item);
        Task RemoveAsync(TEntity item);
    }
}
