using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DAB2_2RDB
{
    public class Repository<T> where T : BaseEntity
    {
        private readonly DbContext _context;

        public Repository(DbContext context)
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
        public void Update(int id, T t)
        {
            if (id != t.Id)
            {
                _context.Add<T>(t);
            }
            else if (_context.Find<T>(t.Id) != null)
            {
                _context.Update(t);
            }
            else
            {
                _context.Add(t);
            }
        }

        /// <summary>
        /// Delete method for repository, removed item for a given Id.
        /// </summary>
        /// <param name="t"></param>
        public void Delete(T t)
        {
            if (_context.Find<T>(t.Id) == null)
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
        public async Task UpdateAsync(int id, T t)
        {
            if (id != t.Id)
            {
                await _context.AddAsync<T>(t);
            }
            else if (await _context.FindAsync<T>(t.Id) != null)
            {
                _context.Update(t);
            }
            else
            {
                await _context.AddAsync(t);
            }
        }
    }

    public class BaseEntity
    {
        public int Id { get; set; }
    }
}