# Observabilidade

O PortalAuth combina health checks, historico operacional, alertas de tela e traces OpenTelemetry.

## Health Checks

Endpoints:

- `/health`: status geral.
- `/health/live`: liveness da aplicacao.
- `/health/ready`: readiness com dependencias criticas.

## Status Operacional

A tela `/Status` executa health checks, registra snapshot em `portal_auth.operational_status_snapshots` e apresenta:

- status geral;
- duracao total;
- componentes individuais;
- historico recente;
- alertas operacionais derivados da ultima checagem e do historico.

## Alertas

Os alertas atuais sao calculados na propria tela:

- componente nao Healthy;
- status geral degradado;
- health check acima de 1 segundo;
- falhas recorrentes nas ultimas checagens.

## OpenTelemetry

Web e API registram tracing com:

- ASP.NET Core instrumentation;
- HttpClient instrumentation;
- resource service name (`Liotecnica.PortalAuth.Web` e `Liotecnica.PortalAuth.Api`);
- exportador OTLP opcional.

Para ativar exportacao OTLP, configure:

```json
{
  "OpenTelemetry": {
    "OtlpEndpoint": "http://localhost:4317"
  }
}
```

Se `OtlpEndpoint` ficar vazio, a aplicacao roda normalmente sem exigir collector local.
