using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Liotecnica.BuildingBlocks.Observability.Extensions;

public static class HealthCheckExtensions
{
    public static IServiceCollection AddLiotecnicaHealthChecks(this IServiceCollection services)
    {
        services.AddHealthChecks()
            .AddCheck(
                "self",
                () => HealthCheckResult.Healthy("Aplicacao respondendo."),
                tags: ["live", "ready"]);

        return services;
    }
}
