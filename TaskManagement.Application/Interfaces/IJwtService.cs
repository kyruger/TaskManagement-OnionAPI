using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagent.Domain.Entities.Concrete;

namespace TaskManagement.Application.Interfaces
{
    public interface IJwtService
    {
        string GenerateJwtToken(AppUser user);
    }
}
