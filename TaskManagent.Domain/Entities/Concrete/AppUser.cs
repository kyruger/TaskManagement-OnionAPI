using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagent.Domain.Entities.Abstract;

namespace TaskManagent.Domain.Entities.Concrete
{
    public class AppUser:BaseEntity
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        // Navigation property 
        public IEnumerable<TaskItem> Tasks { get; set; }
    }
}
