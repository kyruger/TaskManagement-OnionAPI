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
            rt.Token == token);
        }

        public async Task RevokeAllUserTokens(int UserId)
        {
            var tokens = await _context.RefreshTokens
                .Where(rt => rt.AppUserId == UserId && !rt.IsRevoked)
                .ToListAsync();

            foreach (var token in tokens)
            {
                token.IsRevoked = true;
                token.RevokedAt = DateTime.UtcNow;
            }

        }
    }
}
