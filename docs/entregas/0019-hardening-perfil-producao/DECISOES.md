# Decisoes - Entrega 0019

## Role customizada

Foi criado `ApplicationRole` herdando de `IdentityRole<Guid>` para manter compatibilidade com ASP.NET Core Identity e adicionar campos corporativos (`IsActive`, `IsDeleted`, `UpdatedAt`, `UpdatedBy`).

## Troca obrigatoria de senha

O reset administrativo marca `MustChangePassword = true`. Apos login, ou em qualquer navegacao autenticada, o usuario e redirecionado para `Account/ChangePassword` ate concluir a troca.

## Hardening gradual

Foi aplicado rate limiting global e CSP inicial conservadora. A politica ainda permite `style-src 'unsafe-inline'` porque as telas Razor atuais dependem de estilos inline/Bootstrap local.

## PostgreSQL dedicado

O script local passa a usar `postgres` apenas para administracao e grava a connection string com `portalauth_app`.

## Observabilidade

O historico operacional foi implementado como snapshot simples em banco. Alertas ativos e OpenTelemetry ficam como evolucao posterior.
