using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Entities.Concrete;

namespace TaskManagement.Domain.Entities.Concrete
{
    public class AppUser:IdentityUser<int>
    {
        public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    }
}
