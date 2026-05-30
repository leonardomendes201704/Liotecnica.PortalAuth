using Liotecnica.PortalAuth.Application.Interfaces;
using Liotecnica.PortalAuth.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Liotecnica.PortalAuth.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddPortalAuthApplication(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
        });

        services.AddScoped<IPlatformStatusService, PlatformStatusService>();

        return services;
    }
}
