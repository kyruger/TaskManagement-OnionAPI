using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Application.DTOs.Auth
{
    public class TokenResponseDTO
    {
        public string AccessToken { get; set; } = null!;
    }
}
