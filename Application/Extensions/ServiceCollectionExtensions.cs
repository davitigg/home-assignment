using Application.Common.Settings;
using Application.Interfaces;
using Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<HashingSettings>(configuration.GetSection(HashingSettings.SectionName));
            services.Configure<ExpirationSettings>(configuration.GetSection(ExpirationSettings.SectionName));

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IDeviceService, DeviceService>();
            services.AddScoped<IOtpService, OtpService>();
            services.AddScoped<IHashingService, HashingService>();

            return services;
        }
    }
}