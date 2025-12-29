using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Infrastructure.Data;
using TaskManagement.Domain.Entities.Concrete;
using TaskManagement.Domain.Interfaces;

namespace TaskManagement.Infrastructure.Repositorys
{
    public class TaskRepository: GenericRepository<TaskItem>, ITaskRepository
    {
        public TaskRepository(AppDbContext context):base(context)
        {
            
        }

        public async Task<IEnumerable<TaskItem>> GetTasksByUserIdAsync(int userId)
        {
            return await _context.Tasks
                .Where(t => t.AppUserId == userId && !t.IsDeleted)
                .ToListAsync();
        }
    }
}
    