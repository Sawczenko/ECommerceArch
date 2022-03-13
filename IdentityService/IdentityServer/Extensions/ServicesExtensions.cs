using Core.Entities;
using Core.Interfaces;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddScopedServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<ITokenService, TokenService>();
            return services;
        }

        public static IServiceCollection AddRequiredServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            return services;
        }
    }
}