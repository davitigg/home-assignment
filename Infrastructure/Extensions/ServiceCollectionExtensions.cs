using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration Configuration)
        {

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase(Configuration.GetConnectionString("ConnectionDbDefault")));

            services.AddScoped<IUserRepository, UserRepository>();
            
            services.AddMemoryCache();
            services.AddScoped<IMemoryCacheService, MemoryCacheService>();

            return services;
        }
    }
}