using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Entities.Abstract;
using TaskManagement.Domain.Entities.Concrete;

namespace TaskManagement.Domain.Entities.Concrete
{
    public class RefreshToken:BaseEntity
    {
        public string Token { get; set; } = null!;
        public DateTime Expires { get; set; }
        public bool IsRevoked { get; set; }
        public DateTime? RevokedAt { get; set; }

        //Navigation
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; } = null!;
    }
}
