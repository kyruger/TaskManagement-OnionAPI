using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Entities.Concrete;

namespace TaskManagement.Domain.Interfaces
{
    public interface IRefreshTokenHelper
    {
        RefreshToken GenerateRefreshToken(int UserId);
    }
}
