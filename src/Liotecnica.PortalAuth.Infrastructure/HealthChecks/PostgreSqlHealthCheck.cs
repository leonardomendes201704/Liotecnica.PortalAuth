using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Npgsql;

namespace Liotecnica.PortalAuth.Infrastructure.HealthChecks;

public sealed class PostgreSqlHealthCheck : IHealthCheck
{
    private readonly string _connectionString;

    public PostgreSqlHealthCheck(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("PortalAuth")
            ?? throw new InvalidOperationException("Connection string 'PortalAuth' nao configurada.");
    }

    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync(cancellationToken);

            await using var command = new NpgsqlCommand("SELECT 1;", connection);
            await command.ExecuteScalarAsync(cancellationToken);

            return HealthCheckResult.Healthy("PostgreSQL respondeu com sucesso.");
        }
        catch (Exception exception)
        {
            return HealthCheckResult.Unhealthy("Falha ao validar conexao com PostgreSQL.", exception);
        }
    }
}
