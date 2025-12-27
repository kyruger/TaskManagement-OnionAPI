using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.DTOs.Auth;
using TaskManagement.Application.Interfaces;
using TaskManagent.Domain.Entities.Concrete;
using TaskManagent.Domain.Interfaces;


namespace TaskManagement.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtService _jwtService;



        public AuthService(UserManager<AppUser> userManager, IUnitOfWork unitOfWork, IJwtService jwtService)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
        }
        public async Task<string?> LoginAsync(LoginDTO model)
        {
            var user =await _userManager.FindByNameAsync(model.UserName);

            if (user==null)
            {
                return null;
            }

            var valid = await _userManager.CheckPasswordAsync(user, model.Password);

            if (!valid)
            {
                return null;
            }
            return _jwtService.GenerateJwtToken(user);
        }

        public async Task<string> RefreshAccessTokenAsync(string refreshToken)
        {
            var token = await _unitOfWork.RefreshTokens.GetValidTokenWithUserAsync(refreshToken);

            if (token == null)
            {
                throw new UnauthorizedAccessException();
            }

            return _jwtService.GenerateJwtToken(token.AppUser);
        }

        public async Task<bool> RegisterAsync(RegisterDTO model)
        {
            var user = new AppUser
            {
                UserName = model.UserName,
                Email = model.Email
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            return result.Succeeded;

        }
    }
    
}
