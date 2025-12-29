using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Entities.Concrete;
using TaskManagement.Domain.Interfaces;

namespace TaskManagement.Infrastructure.Helpers
{
    public class RefreshTokenHelper : IRefreshTokenHelper
    {
        private  string GenerateToken()
        {
            var randomBytes = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);

        }
        public RefreshToken GenerateRefreshToken(int UserId)
        {
            return new RefreshToken
            {
                Token = GenerateToken(),
                Expires = DateTime.UtcNow.AddDays(7),
                AppUserId = UserId,
            };
        }
    }
}
