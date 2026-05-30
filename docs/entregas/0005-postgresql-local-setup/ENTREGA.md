# Entrega 0005 - Setup Local PostgreSQL

## Objetivo

Verificar o PostgreSQL local e preparar o projeto para usar o banco PostgreSQL existente na maquina.

## Diagnostico

Detectado:

- PostgreSQL 17 instalado em `C:\Program Files\PostgreSQL\17\bin`.
- Servidor aceitando conexoes em `localhost:5432`.
- Usuario `postgres` exige senha.
- Docker tambem esta instalado, mas a decisao foi usar o PostgreSQL local existente.

## Escopo Entregue

- Inicializado User Secrets no projeto `Liotecnica.PortalAuth.Api`.
- Criado script `scripts/Setup-LocalPostgres.ps1`.
- Documentado setup local em `docs/DATABASE.md`.
- Atualizado `docs/SECURITY.md` com regra de senha fora do versionamento.

## Como Usar

Defina a senha local do usuario PostgreSQL:

```powershell
$env:PORTALAUTH_POSTGRES_PASSWORD = "SUA_SENHA_LOCAL"
```

Execute:

```powershell
.\scripts\Setup-LocalPostgres.ps1
```

O script:

- valida conexao com PostgreSQL;
- cria o banco `liotecnica_portalauth` se necessario;
- grava a connection string em User Secrets;
- restaura `dotnet-ef`;
- aplica as migrations.

## Fora do Escopo

- Alterar senha do usuario `postgres`.
- Alterar `pg_hba.conf`.
- Criar usuario dedicado para o projeto.
- Rodar o script sem a senha local disponivel.

## Proximo Passo

Depois de configurar a senha local, executar o script e validar o banco com a migration aplicada.
