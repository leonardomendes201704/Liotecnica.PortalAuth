# Liotecnica.PortalAuth

Portal corporativo de autenticacao e autorizacao da Liotecnica.

Este repositorio inicia a fundacao tecnica descrita em `PROPOSTA_ARQUITETURA_CORPORATIVA_TI.md`: solucao .NET 8, MVC Razor, API REST, Building Blocks reutilizaveis, logging, health checks, correlation ID e documentacao de governanca.

Padroes definidos:

- APIs em camadas com `Controller -> MediatR -> Handler -> Service -> Interface -> Models/Enums`.
- Swagger acessivel em desenvolvimento e aberto automaticamente no browser ao iniciar a API.
- PostgreSQL como banco oficial.

## Estrutura

```text
/src
  /Liotecnica.PortalAuth.Web
  /Liotecnica.PortalAuth.Api
  /Liotecnica.PortalAuth.Application
  /Liotecnica.PortalAuth.Domain
  /Liotecnica.PortalAuth.Infrastructure
  /Liotecnica.BuildingBlocks.*
/tests
  /Liotecnica.PortalAuth.UnitTests
/docs
  ROADMAP.md
  BACKLOG.md
  ARCHITECTURE.md
  DATABASE.md
  SECURITY.md
  /entregas
```

## Comandos

```powershell
dotnet restore .\Liotecnica.PortalAuth.slnx
dotnet build .\Liotecnica.PortalAuth.slnx
dotnet test .\Liotecnica.PortalAuth.slnx
dotnet tool restore
dotnet run --project .\src\Liotecnica.PortalAuth.Api\Liotecnica.PortalAuth.Api.csproj
dotnet run --project .\src\Liotecnica.PortalAuth.Web\Liotecnica.PortalAuth.Web.csproj
```

Ao iniciar a API com o perfil de desenvolvimento, o browser abre em `/swagger`.

## Login Local

Em ambiente de desenvolvimento, o Web cria um usuario administrador inicial ao iniciar a aplicacao:

```text
E-mail: admin@liotecnica.com.br
Senha: configurada em User Secrets na chave Seed:AdminPassword
```

No ambiente local atual, a senha foi configurada via User Secrets para teste.

## Documentacao

- `docs/ROADMAP.md`: fases e proximo passo.
- `docs/BACKLOG.md`: ideias, pendencias e melhorias.
- `docs/ARCHITECTURE.md`: arquitetura implementada.
- `docs/DATABASE.md`: PostgreSQL, EF Core e migrations.
- `docs/SECURITY.md`: baseline de seguranca.
- `docs/entregas/`: historico de entregas documentadas.
