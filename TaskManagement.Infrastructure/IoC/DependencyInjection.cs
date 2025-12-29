using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Infrastructure.Data;
using TaskManagement.Infrastructure.Repositorys;
using TaskManagement.Domain.Entities.Concrete;
using TaskManagement.Domain.Interfaces;
using TaskManagement.Infrastructure.Helpers;

namespace TaskManagement.Infrastructure.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IRefreshTokenHelper, RefreshTokenHelper>();

            services.AddIdentityCore<AppUser>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
            })
            .AddRoles<IdentityRole<int>>()
            .AddEntityFrameworkStores<AppDbContext>();

            return services;
        }
    }
}
