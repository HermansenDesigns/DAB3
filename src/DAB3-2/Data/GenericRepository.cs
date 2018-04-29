using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DAB32.Models;
using Microsoft.EntityFrameworkCore;

namespace DAB2_2RDB
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected F184DABH2Gr24Context _context;

        public GenericRepository(F184DABH2Gr24Context context)
        {
            _context = context;
        }


        public virtual T Add(T t)
        {
            _context.Set<T>().Add(t);
            return t;
        }

        public virtual async Task<T> AddAsync(T t)
        {
            await _context.Set<T>().AddAsync(t);
            return t;
        }

        public virtual int Count()
        {
            return _context.Set<T>().Count();
        }

        public virtual async Task<int> CountAsync()
        {
            return await _context.Set<T>().CountAsync();

        }

        public virtual void Delete(T t)
        {
            _context.Set<T>().Remove(t);
        }

        private bool _disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (this._disposed) return;
            if (disposing)
            {
                _context.Dispose();
            }

            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual T Find(Expression<Func<T, bool>> match)
        {
            return _context.Set<T>().SingleOrDefault(match);
        }

        public virtual ICollection<T> FindAll(Expression<Func<T, bool>> match)
        {
            return _context.Set<T>().Where(match).ToList();
        }

        public virtual async Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match)
        {
            return await _context.Set<T>().Where(match).ToListAsync();
        }

        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        public virtual async Task<ICollection<T>> FindByAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public virtual T Get(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public virtual IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public virtual async Task<ICollection<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public virtual IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> queryable = GetAll();
            foreach (Expression<Func<T, object>> includeProperty in includeProperties)
            {
                queryable = queryable.Include<T, object>(includeProperty);
            }

            return queryable;
        }

        public virtual async Task<T> GetAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public virtual T Update(T t, object key)
        {
            if (t == null)
                return null;
            T exist = _context.Set<T>().Find(key);
            if (exist != null)
                _context.Entry(exist).CurrentValues.SetValues(t);

            return exist;
        }

        public virtual async Task<T> UpdateAsync(T t, object key)
        {
            if (t == null)
                return null;
            T exist = await _context.Set<T>().FindAsync(key);
            if (exist != null)
                _context.Entry(exist).CurrentValues.SetValues(t);

            return exist;
        }
    }
}