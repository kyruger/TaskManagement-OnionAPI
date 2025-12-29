using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Interfaces;

namespace TaskManagement.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        ITaskRepository Tasks { get; }
        IRefreshTokenRepository RefreshTokens { get; }
        Task<int> SaveChangesAsync();
    }
}
