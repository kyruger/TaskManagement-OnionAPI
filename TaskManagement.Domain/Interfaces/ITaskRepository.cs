using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagent.Domain.Entities.Concrete;

namespace TaskManagent.Domain.Interfaces
{
    public interface ITaskRepository: IGenericRepository<TaskItem>
    {
        Task<IEnumerable<TaskItem>> GetTasksByUserIdAsync(int userId);
    }
}
