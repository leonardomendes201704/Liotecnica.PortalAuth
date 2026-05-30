# Banco de Dados

## Banco Oficial

O banco oficial do PortalAuth e PostgreSQL.

## EF Core

A persistencia usa Entity Framework Core com provider Npgsql:

- `Microsoft.EntityFrameworkCore`
- `Microsoft.EntityFrameworkCore.Design`
- `Npgsql.EntityFrameworkCore.PostgreSQL`
- ferramenta local `dotnet-ef`

## DbContext

DbContext principal:

```text
src/Liotecnica.PortalAuth.Infrastructure/Persistence/PortalAuthDbContext.cs
```

Schema padrao:

```text
portal_auth
```

## Connection String

Chave:

```text
ConnectionStrings:PortalAuth
```

Exemplo local sem senha versionada:

```json
{
  "ConnectionStrings": {
    "PortalAuth": "Host=localhost;Port=5432;Database=liotecnica_portalauth;Username=portalauth_app"
  }
}
```

Quando houver senha, usar User Secrets, variaveis de ambiente ou cofre de segredos.

## Setup Local PostgreSQL

O ambiente local detectado possui PostgreSQL instalado em:

```text
C:\Program Files\PostgreSQL\17\bin
C:\Program Files\PostgreSQL\18\bin
```

O servidor local responde em:

```text
localhost:5432
```

Como o usuario `postgres` exige senha e deve ficar restrito a administracao, o setup local cria/usa um usuario dedicado `portalauth_app`. Defina as senhas em variaveis de ambiente apenas na sua maquina:

```powershell
$env:PORTALAUTH_POSTGRES_PASSWORD = "SUA_SENHA_LOCAL"
$env:PORTALAUTH_APP_POSTGRES_PASSWORD = "SENHA_DO_USUARIO_APP"
```

Depois execute:

```powershell
.\scripts\Setup-LocalPostgres.ps1
```

O script faz:

- valida se o PostgreSQL local esta aceitando conexoes;
- cria o usuario dedicado `portalauth_app` se ainda nao existir;
- cria o banco `liotecnica_portalauth` se ainda nao existir;
- define `portalauth_app` como owner do banco local;
- grava `ConnectionStrings:PortalAuth` em User Secrets do projeto API;
- restaura a ferramenta local `dotnet-ef`;
- aplica as migrations no PostgreSQL.

Para conferir a connection string configurada em User Secrets:

```powershell
dotnet user-secrets list --project .\src\Liotecnica.PortalAuth.Api\Liotecnica.PortalAuth.Api.csproj
```

## Recuperar Senha Local Do PostgreSQL

Se o PostgreSQL local estiver instalado mas a senha do usuario `postgres` tiver sido perdida, execute em um PowerShell aberto como Administrador:

```powershell
.\scripts\Reset-LocalPostgresPassword.ps1 -Version 18 -NewPassword "NOVA_SENHA_LOCAL"
```

O script:

- faz backup temporario do `pg_hba.conf`;
- adiciona `trust` local apenas para o usuario `postgres`;
- reinicia o servico PostgreSQL;
- altera a senha do usuario `postgres`;
- restaura o `pg_hba.conf` original;
- reinicia o servico novamente;
- valida login com a nova senha.

Depois disso, execute `scripts\Setup-LocalPostgres.ps1` para configurar o banco do projeto e aplicar as migrations.

## Ferramenta Local

Restaurar ferramenta:

```powershell
dotnet tool restore
```

## Migrations

Criar migration:

```powershell
dotnet tool run dotnet-ef migrations add NomeDaMigration `
  --project .\src\Liotecnica.PortalAuth.Infrastructure\Liotecnica.PortalAuth.Infrastructure.csproj `
  --startup-project .\src\Liotecnica.PortalAuth.Api\Liotecnica.PortalAuth.Api.csproj `
  --context PortalAuthDbContext `
  --output-dir Persistence\Migrations
```

Aplicar migration no banco:

```powershell
dotnet tool run dotnet-ef database update `
  --project .\src\Liotecnica.PortalAuth.Infrastructure\Liotecnica.PortalAuth.Infrastructure.csproj `
  --startup-project .\src\Liotecnica.PortalAuth.Api\Liotecnica.PortalAuth.Api.csproj `
  --context PortalAuthDbContext
```

Remover ultima migration ainda nao aplicada:

```powershell
dotnet tool run dotnet-ef migrations remove `
  --project .\src\Liotecnica.PortalAuth.Infrastructure\Liotecnica.PortalAuth.Infrastructure.csproj `
  --startup-project .\src\Liotecnica.PortalAuth.Api\Liotecnica.PortalAuth.Api.csproj `
  --context PortalAuthDbContext
```

## Migration Inicial

Migration:

```text
InitialPortalAuthSchema
```

Ela cria:

- schema `portal_auth`;
- tabela `corporate_systems`;
- indice unico para `code`.

## Migrations Aplicadas

- `InitialPortalAuthSchema`: cria schema inicial e tabela de sistemas.
- `AddIdentityAndDashboard`: adiciona Identity e dados do dashboard.
- `AddAdminModules`: adiciona permissoes e vinculos de permissao por perfil.
- `AddRoleSystemAccess`: adiciona `portal_auth.role_system_access`, vinculando perfis a sistemas.
- `AddAuditLogs`: adiciona `portal_auth.audit_logs`, armazenando trilha de auditoria.
- `AddAppSettingsAndAuditRetention`: adiciona `portal_auth.app_settings`, armazenando configuracoes administraveis.

## Controle de Acesso a Sistemas

A tabela `portal_auth.role_system_access` possui chave composta:

```text
role_id + system_id
```

Ela define quais sistemas cada perfil pode visualizar no dashboard. Um usuario herda os sistemas acessiveis a partir dos perfis vinculados em `AspNetUserRoles`.

## Auditoria

A tabela `portal_auth.audit_logs` registra eventos relevantes de autenticacao e administracao.

Campos principais:

- `occurred_at`;
- `action`;
- `entity_name`;
- `entity_id`;
- `user_id`;
- `user_name`;
- `ip_address`;
- `correlation_id`;
- `details`.

Indices foram criados para apoiar consultas por data, acao, entidade e usuario.

## Configuracoes Persistidas

A tabela `portal_auth.app_settings` armazena configuracoes administraveis da plataforma.

Campos principais:

- `key`;
- `value`;
- `description`;
- `updated_at`;
- `updated_by`.

Configuracao inicial:

```text
Audit.RetentionDays = 180
```

Essa configuracao define a quantidade de dias de logs de auditoria mantidos antes da limpeza.
