using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Interfaces;
using TaskManagement.Infrastructure.Data;

namespace TaskManagement.Infrastructure.Repositorys
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;

            Tasks = new TaskRepository(_context);

            RefreshTokens = new RefreshTokenRepository(_context);
        }

        public ITaskRepository Tasks { get; }
        public IRefreshTokenRepository RefreshTokens { get; }

        public async Task<int> SaveChangesAsync()
        {
          return await _context.SaveChangesAsync();
        }
    }
}
