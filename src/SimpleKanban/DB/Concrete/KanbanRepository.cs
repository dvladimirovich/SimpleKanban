using Microsoft.EntityFrameworkCore;
using SimpleKanban.DB.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SimpleKanban.DB.Concrete
{
    class KanbanRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        KanbanContext _context;
        DbSet<TEntity> _dbSet;

        public KanbanRepository(KanbanContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public TEntity FindById(int id)
        {
            return _dbSet.Find(id);
        }

        public IQueryable<TEntity> Get()
        {
            return _dbSet.AsNoTracking();
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate);
        }

        public int Count()
        {
            return _dbSet.Count();
        }

        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Count(predicate);
        }

        public IQueryable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] properties)
        {
            return Include(properties);
        }

        public IQueryable<TEntity> GetWithInclude(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] properties)
        {
            var query = Include(properties);
            return query.Where(predicate);
        }

        public async Task<TEntity> FindByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IQueryable<TEntity>> GetAsync()
        {
            return await Task.Run(() => _dbSet.AsNoTracking());
        }

        public async Task<IQueryable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Task.Run(() => _dbSet.AsNoTracking().Where(predicate));
        }

        public async Task<int> CountAsync()
        {
            return await _dbSet.CountAsync();
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.CountAsync(predicate);
        }

        public async Task<IQueryable<TEntity>> GetWithIncludeAsync(params Expression<Func<TEntity, object>>[] properties)
        {
            return await IncludeAsync(properties);
        }

        public async Task<IQueryable<TEntity>> GetWithIncludeAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] properties)
        {
            var query = await IncludeAsync(properties);
            return await Task.Run(() => query.Where(predicate));
        }

        // Для загрузки связанных данных
        private IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] properties)
        {
            IQueryable<TEntity> query = _dbSet.AsNoTracking();
            return properties.Aggregate(query, (current, property) => current.Include(property));
        }

        // Для загрузки связанных данных
        private async Task<IQueryable<TEntity>> IncludeAsync(params Expression<Func<TEntity, object>>[] properties)
        {
            IQueryable<TEntity> query = await Task.Run(() => _dbSet.AsNoTracking());
            return properties.Aggregate(query, (current, property) => current.Include(property));
        }

        public void Create(TEntity item)
        {
            _dbSet.Add(item);
            _context.SaveChanges();
        }

        public void Update(TEntity item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Remove(TEntity item)
        {
            _dbSet.Remove(item);
            _context.SaveChanges();
        }

        public async Task CreateAsync(TEntity item)
        {
            _dbSet.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(TEntity item)
        {
            _dbSet.Remove(item);
            await _context.SaveChangesAsync();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    _context.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~EFUnitOfWork() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
