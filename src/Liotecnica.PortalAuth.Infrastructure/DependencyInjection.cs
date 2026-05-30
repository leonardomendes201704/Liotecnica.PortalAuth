using Liotecnica.PortalAuth.Infrastructure.HealthChecks;
using Liotecnica.PortalAuth.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Liotecnica.PortalAuth.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddPortalAuthInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("PortalAuth");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException("Connection string 'PortalAuth' nao configurada.");
        }

        services.AddDbContext<PortalAuthDbContext>(options =>
        {
            options.UseNpgsql(
                connectionString,
                npgsqlOptions => npgsqlOptions.MigrationsAssembly(typeof(PortalAuthDbContext).Assembly.FullName));
        });

        services.AddHealthChecks()
            .AddCheck<PostgreSqlHealthCheck>(
                "postgresql",
                failureStatus: HealthStatus.Unhealthy,
                tags: ["ready", "database"]);

        return services;
    }
}
