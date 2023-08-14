using DevsuTest.Application.Services.Implementations;
using DevsuTest.Application.Services.Interfaces;
using DevsuTest.Core.Interfaces;
using DevsuTest.Core.Security;
using DevsuTest.Repository.UOW;
using DevsuTest.Repository.Base;
using DevsuTest.Repository.Reportes;

namespace DevsuTest.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
        {
            services.AddTransient<IPasswordHasher, PasswordHasher>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IReportesRepository, ReportesRepository>();

            services.AddScoped<IClientesService, ClientesService>();
            services.AddScoped<ICuentasService, CuentasService>();
            services.AddScoped<IMovimientosService, MovimientosService>();
            services.AddScoped<IReportesService, ReportesService>();

            return services;
        }
    }
}
