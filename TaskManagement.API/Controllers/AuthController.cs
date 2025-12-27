using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.DTOs.Auth;
using TaskManagement.Application.Interfaces;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO model)
        {
            var result = await _authService.RegisterAsync(model);
            return result ? Ok() : BadRequest("Registration failed");
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            var token = await _authService.LoginAsync(model);
            return token == null ? Unauthorized() : Ok(new { token });
        }
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken(TokenRequestDTO model)
        {
            var token = await _authService.RefreshAccessTokenAsync(model.RefreshToken);

            return Ok(new TokenResponseDTO 
            {
                AccessToken = token
            });

        }
    }
}
