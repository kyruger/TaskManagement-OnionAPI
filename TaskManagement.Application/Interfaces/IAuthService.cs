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
        Task<TokenResponseDTO> LogInAsync(LoginDTO model);   

        Task<TokenResponseDTO> RefreshAccessTokenAsync(string refreshToken);

    }
}
