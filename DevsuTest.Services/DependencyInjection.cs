using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DevsuTest.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg => { cfg.AllowNullCollections = true; },
                Assembly.GetExecutingAssembly()
            );

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
