using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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
            var result = await _authService.LogInAsync(model);

            if (result.AccessToken == null)
            {
                return Unauthorized();
            }

            Response.Cookies.Append("refreshToken",result.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddDays(7)
            });

            return Ok(new
            {
                AccessToken = result.AccessToken
            });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(refreshToken))
            {
                return Unauthorized();  
            }

            var result = await _authService.RefreshAccessTokenAsync(refreshToken);  

            Response.Cookies.Append("refreshToken", result.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddDays(7)
            });
                
            return Ok(new
            {
                AccessToken = result.AccessToken
            });  
        }
    }
}
