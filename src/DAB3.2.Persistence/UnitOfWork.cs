using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DAB2_2RDB
{
    public class UnitOfWork
    {
        public DbContext Context { get; set; }
        public Repository<Person> Repository { get; set; }




        public UnitOfWork(DbContext context)
        {
            Context = context;
        }

        public async Task SaveAsync()
        {
            await Context.SaveChangesAsync();
        }

        public void Save()
        {
            Context.SaveChanges();
        }
    }
}
