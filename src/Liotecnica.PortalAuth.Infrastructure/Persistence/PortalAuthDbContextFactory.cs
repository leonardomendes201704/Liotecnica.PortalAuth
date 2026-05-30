using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Liotecnica.PortalAuth.Infrastructure.Persistence;

public sealed class PortalAuthDbContextFactory : IDesignTimeDbContextFactory<PortalAuthDbContext>
{
    public PortalAuthDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<PortalAuthDbContext>();
        var password = Environment.GetEnvironmentVariable("PORTALAUTH_POSTGRES_PASSWORD");
        var connectionString = string.IsNullOrWhiteSpace(password)
            ? "Host=localhost;Port=5432;Database=liotecnica_portalauth;Username=postgres"
            : $"Host=localhost;Port=5432;Database=liotecnica_portalauth;Username=postgres;Password={password}";

        optionsBuilder.UseNpgsql(
            connectionString,
            options => options.MigrationsAssembly(typeof(PortalAuthDbContext).Assembly.FullName));

        return new PortalAuthDbContext(optionsBuilder.Options);
    }
}
