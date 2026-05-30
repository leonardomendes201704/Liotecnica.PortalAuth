# Entrega 0003 - EF Core e Migrations

## Objetivo

Completar a fundacao de persistencia com EF Core, migrations versionadas e PostgreSQL.

## Escopo Entregue

- Adicionado `Microsoft.EntityFrameworkCore.Design`.
- Criado manifesto local de ferramenta em `dotnet-tools.json`.
- Instalado `dotnet-ef` local na versao `8.0.27`.
- Criada entidade de dominio `CorporateSystem`.
- Criado mapeamento EF Core `CorporateSystemConfiguration`.
- Atualizado `PortalAuthDbContext` com `DbSet<CorporateSystem>`.
- Criado `PortalAuthDbContextFactory` para design-time.
- Configurado migrations assembly no provider Npgsql.
- Gerada migration inicial `InitialPortalAuthSchema`.
- Criado `docs/DATABASE.md` com comandos de migrations.

## Migration Inicial

Arquivo:

```text
src/Liotecnica.PortalAuth.Infrastructure/Persistence/Migrations/20260530114123_InitialPortalAuthSchema.cs
```

Cria:

- schema `portal_auth`;
- tabela `corporate_systems`;
- indice unico para `code`.

## Fora do Escopo

- Aplicar migration em banco real.
- Criar container PostgreSQL.
- Criar seed.
- Implementar Identity.

## Como Validar

```powershell
dotnet tool restore
dotnet build .\Liotecnica.PortalAuth.slnx --no-restore
dotnet test .\Liotecnica.PortalAuth.slnx --no-build
```

Para aplicar no banco local quando o PostgreSQL estiver disponivel:

```powershell
dotnet tool run dotnet-ef database update `
  --project .\src\Liotecnica.PortalAuth.Infrastructure\Liotecnica.PortalAuth.Infrastructure.csproj `
  --startup-project .\src\Liotecnica.PortalAuth.Api\Liotecnica.PortalAuth.Api.csproj `
  --context PortalAuthDbContext
```
