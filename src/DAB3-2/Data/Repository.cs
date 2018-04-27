using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DAB2_2RDB
{
    public class Repository<T> where T : class
    {
        private readonly DbContext _context;

        public Repository(DbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Syncronous method for Create
        /// </summary>
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
        public void Update(int id, T t)
        {
            var toUpdate = _context.Find<T>(id);
            toUpdate = t;
            _context.Update(toUpdate);
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

        public IEnumerable<T> ReadAll()
        {
            return (IEnumerable<T>) _context.ChangeTracker.Entries<T>();
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
        public async Task<T> ReadAsync(int id)
        {
            return await _context.FindAsync<T>(id);
        }
    }
}