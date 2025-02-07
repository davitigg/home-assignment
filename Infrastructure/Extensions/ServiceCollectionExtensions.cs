using Domain.Interfaces;
using Infrastructure.Configuration;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DbSettings>(configuration.GetSection(DbSettings.SectionName));

            services.AddDbContext<ApplicationDbContext>();
            services.AddSingleton<MemoryCache>();

            services.AddScoped<IOtpRepository, OtpRepository>();
            services.AddScoped<IPendingDeviceRepository, PendingDeviceRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IDeviceRepository, DeviceRepository>();

            return services;
        }
    }
}