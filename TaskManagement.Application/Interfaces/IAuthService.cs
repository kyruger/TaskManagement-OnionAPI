using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.DTOs.Auth;

namespace TaskManagement.Application.Interfaces
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(RegisterDTO model);
        Task<string?> LoginAsync(LoginDTO model);   

        Task<string> RefreshAccessTokenAsync(string refreshToken);

    }
}
