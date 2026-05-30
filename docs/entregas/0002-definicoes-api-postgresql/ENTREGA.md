# Entrega 0002 - Definicoes de API e PostgreSQL

## Objetivo

Formalizar e aplicar tres definicoes obrigatorias:

- APIs devem ter Swagger acessivel e abrir no browser em desenvolvimento.
- APIs devem seguir arquitetura em camadas com Controller, MediatR, Models, Services, Interfaces e Enums.
- Banco oficial da plataforma deve ser PostgreSQL.

## Escopo Entregue

- Adicionado MediatR na camada Application/API.
- Endpoint `GET /api/v1/platform/status` movido de minimal API para `PlatformController`.
- Criados `GetPlatformStatusQuery` e `GetPlatformStatusQueryHandler`.
- Criados `PlatformStatusModel`, `PlatformStatusService`, `IPlatformStatusService` e `PlatformComponentStatus`.
- Adicionado EF Core com provider PostgreSQL/Npgsql compatibivel com `net8.0`.
- Criado `PortalAuthDbContext` com schema padrao `portal_auth`.
- Criada extensao `AddPortalAuthInfrastructure`.
- Atualizada connection string `PortalAuth` sem senha versionada.
- Reforcada configuracao do Swagger em desenvolvimento.
- Atualizados `README.md`, `ARCHITECTURE.md`, `SECURITY.md`, `ROADMAP.md` e `BACKLOG.md`.

## Fora do Escopo

- Criar migrations.
- Criar tabelas reais.
- Subir container PostgreSQL.
- Implementar Identity.
- Criar health check de banco.

## Como Validar

```powershell
dotnet restore .\Liotecnica.PortalAuth.slnx
dotnet build .\Liotecnica.PortalAuth.slnx --no-restore
dotnet test .\Liotecnica.PortalAuth.slnx --no-build
dotnet run --project .\src\Liotecnica.PortalAuth.Api\Liotecnica.PortalAuth.Api.csproj
```

Ao iniciar a API em desenvolvimento, o browser deve abrir em `/swagger`.

## Proximo Passo

Implementar Identity e as primeiras entidades persistidas no PostgreSQL.
