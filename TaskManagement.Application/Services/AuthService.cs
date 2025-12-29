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
using TaskManagement.Domain.Entities.Concrete;
using TaskManagement.Domain.Interfaces;


namespace TaskManagement.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtService _jwtService;
        private readonly IRefreshTokenHelper _refreshTokenHelper;


        public AuthService(UserManager<AppUser> userManager, IUnitOfWork unitOfWork, IJwtService jwtService, IRefreshTokenHelper refreshTokenHelper)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
            _refreshTokenHelper = refreshTokenHelper;
        }
        public async Task<TokenResponseDTO?> LogInAsync(LoginDTO model)
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
            var refreshToken = _refreshTokenHelper.GenerateRefreshToken(user.Id);
            await _unitOfWork.RefreshTokens.AddAsync(refreshToken);
            await _unitOfWork.SaveChangesAsync();

            return new TokenResponseDTO
            {
                AccessToken = _jwtService.GenerateJwtToken(user),
                RefreshToken = refreshToken.Token
            };
        }

        public async Task<TokenResponseDTO> RefreshAccessTokenAsync(string refreshToken)
        {
            var token = await _unitOfWork.RefreshTokens.GetValidTokenWithUserAsync(refreshToken);

            if (token == null)
            {
                throw new UnauthorizedAccessException("Refresh token reus detected");
            }

            if (token.IsRevoked)
            {
                await _unitOfWork.RefreshTokens.RevokeAllUserTokens(token.AppUserId);

                await _unitOfWork.SaveChangesAsync();
                throw new UnauthorizedAccessException("Token reus attack detected");
            }

            if (token.Expires < DateTime.UtcNow)
            {
                throw new UnauthorizedAccessException("Refresh token has expired");
            }
            token.IsRevoked = true;
            token.RevokedAt = DateTime.UtcNow;

            var newAccessToken = _jwtService.GenerateJwtToken(token.AppUser);

            var newRefreshToken = _refreshTokenHelper.GenerateRefreshToken(token.AppUserId);

            await _unitOfWork.RefreshTokens.AddAsync(newRefreshToken);
            await _unitOfWork.SaveChangesAsync();

            return new TokenResponseDTO
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken.Token
            };

           
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
