using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Entities.Concrete;
using TaskManagement.Domain.Interfaces;
using TaskManagement.Infrastructure.Data;

namespace TaskManagement.Infrastructure.Repositorys
{
    public class RefreshTokenRepository : GenericRepository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<RefreshToken?> GetValidTokenWithUserAsync(string token)
        {
            return await _context.RefreshTokens
        .Include(rt => rt.AppUser)
        .FirstOrDefaultAsync(rt =>
            rt.Token == token &&
            !rt.IsRevoked &&
            rt.Expires > DateTime.UtcNow);
        }
    }
}
