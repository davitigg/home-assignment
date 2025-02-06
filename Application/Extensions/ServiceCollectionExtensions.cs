using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection  AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<IAuthService, AuthService>();

            return services;
        }
    }
}