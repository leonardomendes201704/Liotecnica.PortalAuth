# Entrega 0013 - Status Operacional

## Objetivo

Adicionar validacao real do PostgreSQL aos health checks e criar uma tela no portal para acompanhar a saude operacional da aplicacao.

## Escopo Implementado

- Criado `PostgreSqlHealthCheck`.
- Registrado health check `postgresql` com tags `ready` e `database`.
- Registrado health check `self` com tags `live` e `ready`.
- Separado `/health/live` para liveness.
- Separado `/health/ready` para readiness.
- Criado `StatusController`.
- Criado `OperationalStatusViewModel`.
- Criada view `Views/Status/Index.cshtml`.
- Adicionado item `Status` no menu principal.
- Link `Ver status` do dashboard passou a abrir a tela de status.

## Resultado

O PortalAuth agora diferencia se a aplicacao esta viva de se ela esta pronta para operar com suas dependencias criticas. A tela de status permite visualizar os componentes registrados, estado, duracao e tags.
