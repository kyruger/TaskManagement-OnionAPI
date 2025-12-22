using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagent.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        ITaskRepository Tasks { get; }
        Task<int> SaveChangesAsync();
    }
}
