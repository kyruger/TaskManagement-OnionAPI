using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.DTOs.Tasks;

namespace TaskManagement.Application.Interfaces
{
    public interface ITaskService   
    {
        Task<IEnumerable<TaskDTO>> GetAllAsync();
        Task<TaskDTO?> GetByIdAsync(int id);
        Task<int> CreateAsync(CreateTaskDTO model);
        Task<bool> UpdateAsync(int id,UpdateTaskDTO model);
        Task<bool> DeleteAsync(int id);


    }
}
