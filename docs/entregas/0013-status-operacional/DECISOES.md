# Decisoes - Entrega 0013

## Health check customizado

Foi criado um health check proprio para PostgreSQL usando `NpgsqlConnection`.

Motivos:

- evita adicionar dependencia externa neste momento;
- usa a mesma connection string `PortalAuth`;
- valida abertura de conexao e execucao de `SELECT 1`;
- permite controlar mensagens e tags de readiness.

## Separacao de liveness e readiness

`/health/live` responde apenas se a aplicacao esta viva.

`/health/ready` valida se a aplicacao esta pronta para receber trafego, incluindo PostgreSQL.

## Tela autenticada

A tela `/Status` foi protegida por autenticacao simples.

Ela nao exige permissao administrativa especifica porque mostra apenas informacoes operacionais basicas e nao expoe secrets, connection strings ou stack traces.

## Proxima evolucao

Em uma etapa futura, a tela pode incluir API externa, cache, filas, storage, historico de disponibilidade e alertas.
