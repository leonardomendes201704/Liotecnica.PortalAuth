# Testes - Entrega 0005

## Validacao Executada

Comandos de diagnostico executados:

- `where.exe psql`
- `where.exe postgres`
- `pg_isready -h localhost -p 5432`
- tentativa de conexao sem senha com `psql`

Resultado:

- PostgreSQL encontrado.
- Servidor aceitando conexoes.
- Conexao sem senha falhou, como esperado, porque o usuario `postgres` exige senha.

## Validacao Do Setup

Depois de redefinir a senha local, foi executado:

```powershell
.\scripts\Setup-LocalPostgres.ps1 -Version 18
```

O setup:

- criou o banco `liotecnica_portalauth`;
- salvou `ConnectionStrings:PortalAuth` em User Secrets;
- restaurou `dotnet-ef`;
- aplicou a migration `InitialPortalAuthSchema`.

Validacao no PostgreSQL:

```powershell
psql -h localhost -p 5432 -U postgres -d liotecnica_portalauth
```

Resultado confirmado:

- schema `portal_auth`;
- tabela `portal_auth.corporate_systems`.

## Resultado

Executado com sucesso:

```powershell
dotnet build .\Liotecnica.PortalAuth.slnx
dotnet test .\Liotecnica.PortalAuth.slnx --no-build
pg_isready -h localhost -p 5432
```

Resultado:

- Script `scripts/Setup-LocalPostgres.ps1` sem erros de sintaxe.
- Build aprovado.
- 0 warnings.
- 0 erros.
- 5 testes aprovados.
- PostgreSQL local aceitando conexoes em `localhost:5432`.

Tentativa adicional:

- Foi testada uma senha informada pelo usuario para o usuario `postgres`.
- A autenticacao falhou em `localhost` e em `127.0.0.1`.
- O script foi ajustado para retornar erro claro quando a autenticacao falhar.

Resolvido: a senha foi redefinida e o setup local foi executado com sucesso.

Tentativa de recuperacao:

- Identificados dois servicos locais: `postgresql-x64-17` e `postgresql-x64-18`.
- A porta `5432` esta associada ao PostgreSQL 18.
- O arquivo `pg_hba.conf` do PostgreSQL 18 esta protegido e exige PowerShell/terminal com permissao de Administrador para edicao.
- Criado `scripts/Reset-LocalPostgresPassword.ps1` para executar a recuperacao com seguranca em modo Administrador.
