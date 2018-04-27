using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DAB2_2RDB
{
    public class UnionRepository<T> where T : class
    {
        private readonly DbContext _context;

        public UnionRepository(DbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Syncronous method for Create
        /// </summary>
        /// <param name="t">type <see cref="BaseEntity"/></param>
        /// <returns></returns>
        public void Create(T t)
        {
            _context.Add<T>(t);
        }

        /// <summary>
        /// Syncronous method for Read
        /// </summary>
        /// <param name="id">Takes an id</param>
        /// <returns><see cref="BaseEntity"/></returns>
        public T Read(int id)
        {
            return _context.Find<T>(id);
        }

        /// <summary>
        /// Syncronous Update
        /// Creates a new entity in DB if id is not equal <see cref="t"/>.Id and if neither is found in db.
        /// </summary>
        /// <param name="id">Id to update</param>
        /// <param name="t">Entity with modified values.</param>
        public void Update(T old, T t)
        {
            try
            {
                if (old != t)
                {
                    _context.Add<T>(t);
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        /// <summary>
        /// Delete method for repository, removed item for a given Id.
        /// </summary>
        /// <param name="t"></param>
        public void Delete(T t)
        {
            if (_context.Find<T>(t) == null)
                throw new ArgumentException("Entity was not found in db.");

            _context.Remove(t);
        }

        // Async Methods

        /// <summary>
        /// Async method for Create
        /// </summary>
        /// <param name="t">type <see cref="BaseEntity"/></param>
        /// <returns></returns>
        public async Task CreateAsync(T t)
        {
            await _context.AddAsync<T>(t);
        }

        /// <summary>
        /// Async method for Read
        /// </summary>
        /// <param name="id">Takes an id</param>
        /// <returns><see cref="BaseEntity"/></returns>
        public async Task<T> ReadAsync(int id)
        {
            return await _context.FindAsync<T>(id);
        }

        /// <summary>
        /// Async Update
        /// Creates a new entity in DB if id is not equal <see cref="t"/>.Id and if neither is found in db.
        /// </summary>
        /// <param name="id">Id to update</param>
        /// <param name="t">Entity with modified values.</param>
        public async Task UpdateAsync(T old, T t)
        {
            try
            {
                if (old != t)
                {
                    await _context.AddAsync<T>(t);
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}
