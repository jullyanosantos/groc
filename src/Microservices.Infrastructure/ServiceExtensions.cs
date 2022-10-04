using Microservices.Domain.Interfaces;
using Microservices.Infrastructure.Data;
using Microservices.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microservices.Infrastructure
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddDatabaseContext(configuration).AddUnitOfWork();
        }

        private static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            return services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        private static IServiceCollection AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddSqlite<AppDbContext>(configuration.GetConnectionString("DefaultConnection"), (options) =>
            {
                //options.MigrationsAssembly("CqrsNinja.Migrations");
            });
        }
    }
}