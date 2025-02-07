using API.Utilities;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Conventions.Add(
                    new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
            });

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }
    }
}
