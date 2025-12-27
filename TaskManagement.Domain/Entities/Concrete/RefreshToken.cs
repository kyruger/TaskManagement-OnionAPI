using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagent.Domain.Entities.Abstract;
using TaskManagent.Domain.Entities.Concrete;

namespace TaskManagement.Domain.Entities.Concrete
{
    public class RefreshToken:BaseEntity
    {
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public bool IsRevoked { get; set; }

        //Navigation
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; } = null!;
    }
}
