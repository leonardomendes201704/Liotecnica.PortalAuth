# Decisoes - Entrega 0002

## Controller + MediatR Como Padrao

APIs devem expor endpoints por Controllers e delegar os casos de uso para MediatR.

Isso mantem Controllers finos e cria um caminho claro para commands, queries, validacoes, handlers e services.

## Application Organizada Por Responsabilidade

A camada Application passa a aceitar estas pastas como padrao:

- `Features`
- `Models`
- `Services`
- `Interfaces`
- `Enums`

## PostgreSQL Como Banco Oficial

PostgreSQL foi definido como banco oficial do PortalAuth.

O provider padrao e `Npgsql.EntityFrameworkCore.PostgreSQL`.

## Sem Senha Versionada

A connection string local foi registrada sem senha. Quando senha for necessaria, usar User Secrets, variaveis de ambiente ou cofre de segredos.

## Swagger Em Desenvolvimento

Swagger deve abrir automaticamente ao iniciar a API em desenvolvimento. Em producao, devera ser protegido, restrito ou desabilitado em uma entrega futura de seguranca.
