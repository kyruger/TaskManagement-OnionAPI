using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Infrastructure.Data;
using TaskManagement.Domain.Interfaces;

namespace TaskManagement.Infrastructure.Repositorys
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> table;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            table = _context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await table.AddAsync(entity);
        }

        public void Delete(T entity)
        {
             table.Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await table.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await table.FindAsync(id);
        }

        public void Update(T entity)
        {
            table.Update(entity);
        }
    }
}
